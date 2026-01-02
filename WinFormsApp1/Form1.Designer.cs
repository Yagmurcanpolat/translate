namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtKaynak = new System.Windows.Forms.TextBox();
            this.txtHedef = new System.Windows.Forms.TextBox();
            this.cmbKaynakDil = new System.Windows.Forms.ComboBox();
            this.cmbHedefDil = new System.Windows.Forms.ComboBox();
            this.btnCevir = new System.Windows.Forms.Button();
            this.btnDegistir = new System.Windows.Forms.Button();
            this.lblKaynak = new System.Windows.Forms.Label();
            this.lblHedef = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtKaynak
            // 
            this.txtKaynak.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtKaynak.Location = new System.Drawing.Point(20, 60);
            this.txtKaynak.Multiline = true;
            this.txtKaynak.Name = "txtKaynak";
            this.txtKaynak.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtKaynak.Size = new System.Drawing.Size(360, 300);
            this.txtKaynak.TabIndex = 0;
            // 
            // txtHedef
            // 
            this.txtHedef.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtHedef.Location = new System.Drawing.Point(420, 60);
            this.txtHedef.Multiline = true;
            this.txtHedef.Name = "txtHedef";
            this.txtHedef.ReadOnly = true;
            this.txtHedef.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHedef.Size = new System.Drawing.Size(360, 300);
            this.txtHedef.TabIndex = 1;
            // 
            // cmbKaynakDil
            // 
            this.cmbKaynakDil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbKaynakDil.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbKaynakDil.FormattingEnabled = true;
            this.cmbKaynakDil.Items.AddRange(new object[] {
            "Türkçe",
            "İngilizce",
            "Almanca",
            "Fransızca"});
            this.cmbKaynakDil.Location = new System.Drawing.Point(20, 25);
            this.cmbKaynakDil.Name = "cmbKaynakDil";
            this.cmbKaynakDil.Size = new System.Drawing.Size(150, 25);
            this.cmbKaynakDil.TabIndex = 2;
            this.cmbKaynakDil.SelectedIndex = 0;
            // 
            // cmbHedefDil
            // 
            this.cmbHedefDil.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHedefDil.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmbHedefDil.FormattingEnabled = true;
            this.cmbHedefDil.Items.AddRange(new object[] {
            "Türkçe",
            "İngilizce",
            "Almanca",
            "Fransızca"});
            this.cmbHedefDil.Location = new System.Drawing.Point(630, 25);
            this.cmbHedefDil.Name = "cmbHedefDil";
            this.cmbHedefDil.Size = new System.Drawing.Size(150, 25);
            this.cmbHedefDil.TabIndex = 3;
            this.cmbHedefDil.SelectedIndex = 1;
            // 
            // btnCevir
            // 
            this.btnCevir.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCevir.Location = new System.Drawing.Point(340, 380);
            this.btnCevir.Name = "btnCevir";
            this.btnCevir.Size = new System.Drawing.Size(120, 40);
            this.btnCevir.TabIndex = 4;
            this.btnCevir.Text = "Çevir";
            this.btnCevir.UseVisualStyleBackColor = true;
            this.btnCevir.Click += new System.EventHandler(this.btnCevir_Click);
            // 
            // btnDegistir
            // 
            this.btnDegistir.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnDegistir.Location = new System.Drawing.Point(380, 25);
            this.btnDegistir.Name = "btnDegistir";
            this.btnDegistir.Size = new System.Drawing.Size(40, 25);
            this.btnDegistir.TabIndex = 5;
            this.btnDegistir.Text = "⇄";
            this.btnDegistir.UseVisualStyleBackColor = true;
            this.btnDegistir.Click += new System.EventHandler(this.btnDegistir_Click);
            // 
            // lblKaynak
            // 
            this.lblKaynak.AutoSize = true;
            this.lblKaynak.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblKaynak.Location = new System.Drawing.Point(20, 7);
            this.lblKaynak.Name = "lblKaynak";
            this.lblKaynak.Size = new System.Drawing.Size(48, 15);
            this.lblKaynak.TabIndex = 6;
            this.lblKaynak.Text = "Kaynak:";
            // 
            // lblHedef
            // 
            this.lblHedef.AutoSize = true;
            this.lblHedef.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHedef.Location = new System.Drawing.Point(630, 7);
            this.lblHedef.Name = "lblHedef";
            this.lblHedef.Size = new System.Drawing.Size(42, 15);
            this.lblHedef.TabIndex = 7;
            this.lblHedef.Text = "Hedef:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 440);
            this.Controls.Add(this.lblHedef);
            this.Controls.Add(this.lblKaynak);
            this.Controls.Add(this.btnDegistir);
            this.Controls.Add(this.btnCevir);
            this.Controls.Add(this.cmbHedefDil);
            this.Controls.Add(this.cmbKaynakDil);
            this.Controls.Add(this.txtHedef);
            this.Controls.Add(this.txtKaynak);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Çeviri Uygulaması";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.TextBox txtKaynak;
        private System.Windows.Forms.TextBox txtHedef;
        private System.Windows.Forms.ComboBox cmbKaynakDil;
        private System.Windows.Forms.ComboBox cmbHedefDil;
        private System.Windows.Forms.Button btnCevir;
        private System.Windows.Forms.Button btnDegistir;
        private System.Windows.Forms.Label lblKaynak;
        private System.Windows.Forms.Label lblHedef;
    }
}
