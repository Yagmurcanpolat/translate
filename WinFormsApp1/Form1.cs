using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        private readonly HttpClient httpClient;
        private readonly Dictionary<string, string> dilKodlari;
        private const string GEMINI_API_KEY = "AIzaSyCoQzrSGIFca7OFVrdug8_k2dIHfoG2dX0";
        private readonly string[] GEMINI_MODELS = new[]
        {
            "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent"
        };

        public Form1()
        {
            InitializeComponent();
            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(30); // 30 saniye timeout
            dilKodlari = new Dictionary<string, string>
            {
                { "Türkçe", "Turkish" },
                { "İngilizce", "English" },
                { "Almanca", "German" },
                { "Fransızca", "French" }
            };
        }

        private async void btnCevir_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtKaynak.Text))
            {
                MessageBox.Show("Lütfen çevrilecek metni girin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbKaynakDil.SelectedItem == null || cmbHedefDil.SelectedItem == null)
            {
                MessageBox.Show("Lütfen kaynak ve hedef dilleri seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbKaynakDil.SelectedItem.ToString() == cmbHedefDil.SelectedItem.ToString())
            {
                txtHedef.Text = txtKaynak.Text;
                return;
            }

            btnCevir.Enabled = false;
            btnCevir.Text = "Çevriliyor...";

            try
            {
                string kaynakDil = dilKodlari[cmbKaynakDil.SelectedItem.ToString()!];
                string hedefDil = dilKodlari[cmbHedefDil.SelectedItem.ToString()!];
                string metin = txtKaynak.Text;

                string ceviri = await Cevir(metin, kaynakDil, hedefDil);
                txtHedef.Text = ceviri;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Çeviri sırasında bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnCevir.Enabled = true;
                btnCevir.Text = "Çevir";
            }
        }

        private void btnDegistir_Click(object sender, EventArgs e)
        {
            if (cmbKaynakDil.SelectedItem == null || cmbHedefDil.SelectedItem == null)
                return;

            string geciciDil = cmbKaynakDil.SelectedItem.ToString()!;
            string geciciMetin = txtKaynak.Text;

            cmbKaynakDil.SelectedItem = cmbHedefDil.SelectedItem;
            cmbHedefDil.SelectedItem = geciciDil;

            txtKaynak.Text = txtHedef.Text;
            txtHedef.Text = geciciMetin;
        }

        private async Task<string> Cevir(string metin, string kaynakDil, string hedefDil)
        {
            // Prompt oluştur - sadece çevrilmiş metni döndürmesini istiyoruz
            string prompt = $"Translate the following text from {kaynakDil} to {hedefDil}. Only return the translated text, nothing else, no explanations, no additional text:\n\n{metin}";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = prompt
                            }
                        }
                    }
                }
            };

            var json = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Birden fazla model dene (quota sorunu olursa alternatif model kullan)
            Exception? lastException = null;
            
            foreach (string apiUrl in GEMINI_MODELS)
            {
                try
                {
                    // Header'ları ekle
                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                    request.Content = content;
                    request.Headers.Add("X-goog-api-key", GEMINI_API_KEY);

                    using (var cts = new CancellationTokenSource(TimeSpan.FromSeconds(25)))
                    {
                        var response = await httpClient.SendAsync(request, cts.Token);
                        
                        if (!response.IsSuccessStatusCode)
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            
                            // 429 (Quota) hatası ise bir sonraki modeli dene
                            if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                            {
                                lastException = new Exception("Quota limiti aşıldı. Alternatif model deneniyor...");
                                continue; // Bir sonraki modeli dene
                            }
                            
                            // Diğer hatalar için detaylı mesaj
                            string errorMessage = "API hatası";
                            try
                            {
                                var errorJson = JsonSerializer.Deserialize<JsonElement>(errorContent);
                                if (errorJson.TryGetProperty("error", out var errorObj2))
                                {
                                    if (errorObj2.TryGetProperty("message", out var errorMsg))
                                    {
                                        errorMessage = errorMsg.GetString() ?? errorMessage;
                                    }
                                }
                            }
                            catch
                            {
                                errorMessage = $"HTTP {(int)response.StatusCode}: {errorContent}";
                            }
                            
                            throw new Exception(errorMessage);
                        }

                        var responseJson = await response.Content.ReadAsStringAsync();
                        
                        // Yanıtın JSON olup olmadığını kontrol et
                        if (string.IsNullOrWhiteSpace(responseJson) || responseJson.TrimStart().StartsWith("<"))
                        {
                            throw new Exception("API'den geçersiz yanıt alındı. Lütfen internet bağlantınızı kontrol edin.");
                        }

                        var result = JsonSerializer.Deserialize<JsonElement>(responseJson);

                        // Hata kontrolü
                        if (result.TryGetProperty("error", out var errorObj))
                        {
                            string errorMessage = "API hatası";
                            if (errorObj.TryGetProperty("message", out var errorMsg))
                            {
                                errorMessage = errorMsg.GetString() ?? errorMessage;
                            }
                            
                            // 429 hatası ise bir sonraki modeli dene
                            if (errorMessage.Contains("quota", StringComparison.OrdinalIgnoreCase) || 
                                errorMessage.Contains("RESOURCE_EXHAUSTED", StringComparison.OrdinalIgnoreCase))
                            {
                                lastException = new Exception("Quota limiti aşıldı. Alternatif model deneniyor...");
                                continue;
                            }
                            
                            throw new Exception(errorMessage);
                        }

                        // Gemini API yanıt yapısı: candidates[0].content.parts[0].text
                        if (result.TryGetProperty("candidates", out var candidates) && candidates.GetArrayLength() > 0)
                        {
                            var firstCandidate = candidates[0];
                            
                            // Safety rating kontrolü
                            if (firstCandidate.TryGetProperty("finishReason", out var finishReason))
                            {
                                string reason = finishReason.GetString() ?? "";
                                if (reason == "SAFETY" || reason == "RECITATION")
                                {
                                    throw new Exception("İçerik güvenlik politikası nedeniyle çevrilemedi.");
                                }
                            }
                            
                            if (firstCandidate.TryGetProperty("content", out var candidateContent))
                            {
                                if (candidateContent.TryGetProperty("parts", out var parts) && parts.GetArrayLength() > 0)
                                {
                                    var firstPart = parts[0];
                                    if (firstPart.TryGetProperty("text", out var text))
                                    {
                                        string ceviri = text.GetString()?.Trim() ?? metin;
                                        return ceviri;
                                    }
                                }
                            }
                        }

                        throw new Exception("Çeviri yanıtı beklenen formatta değil.");
                    }
                }
                catch (TaskCanceledException)
                {
                    lastException = new Exception("İstek zaman aşımına uğradı. Alternatif model deneniyor...");
                    continue;
                }
                catch (HttpRequestException ex)
                {
                    lastException = new Exception($"İnternet bağlantısı hatası: {ex.Message}");
                    continue;
                }
                catch (JsonException ex)
                {
                    lastException = new Exception($"API yanıtı işlenirken hata oluştu: {ex.Message}");
                    continue;
                }
                catch (Exception ex)
                {
                    // Quota hatası değilse direkt fırlat
                    if (!ex.Message.Contains("quota", StringComparison.OrdinalIgnoreCase) && 
                        !ex.Message.Contains("RESOURCE_EXHAUSTED", StringComparison.OrdinalIgnoreCase))
                    {
                        throw;
                    }
                    lastException = ex;
                    continue;
                }
            }

            // Tüm modeller denendi ve başarısız oldu
            if (lastException != null)
            {
                throw new Exception($"Tüm modeller denendi ancak çeviri yapılamadı. Quota limitiniz aşılmış olabilir. Lütfen birkaç dakika sonra tekrar deneyin veya Google Cloud Console'dan quota durumunuzu kontrol edin.");
            }

            throw new Exception("Çeviri yapılamadı. Lütfen tekrar deneyin.");
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            httpClient?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
