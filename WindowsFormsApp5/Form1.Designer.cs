namespace WindowsFormsApp5
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblMovesLeft = new System.Windows.Forms.Label();
            this.btnEasy = new System.Windows.Forms.Button();
            this.btnHard = new System.Windows.Forms.Button();
            this.btnMedium = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblMovesLeft
            // 
            this.lblMovesLeft.AutoSize = true;
            this.lblMovesLeft.Location = new System.Drawing.Point(95, 226);
            this.lblMovesLeft.Name = "lblMovesLeft";
            this.lblMovesLeft.Size = new System.Drawing.Size(0, 13);
            this.lblMovesLeft.TabIndex = 0;
            // 
            // btnEasy
            // 
            this.btnEasy.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnEasy.Location = new System.Drawing.Point(31, 21);
            this.btnEasy.Name = "btnEasy";
            this.btnEasy.Size = new System.Drawing.Size(75, 23);
            this.btnEasy.TabIndex = 1;
            this.btnEasy.Text = "Kolay Mod";
            this.btnEasy.UseVisualStyleBackColor = false;
            this.btnEasy.Click += new System.EventHandler(this.btnEasy_Click_1);
            // 
            // btnHard
            // 
            this.btnHard.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnHard.Location = new System.Drawing.Point(193, 21);
            this.btnHard.Name = "btnHard";
            this.btnHard.Size = new System.Drawing.Size(75, 23);
            this.btnHard.TabIndex = 2;
            this.btnHard.Text = "Zor Mod";
            this.btnHard.UseVisualStyleBackColor = false;
            this.btnHard.Click += new System.EventHandler(this.btnHard_Click);
            // 
            // btnMedium
            // 
            this.btnMedium.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.btnMedium.Location = new System.Drawing.Point(112, 21);
            this.btnMedium.Name = "btnMedium";
            this.btnMedium.Size = new System.Drawing.Size(75, 23);
            this.btnMedium.TabIndex = 3;
            this.btnMedium.Text = "Orta Mod";
            this.btnMedium.UseVisualStyleBackColor = false;
            this.btnMedium.Click += new System.EventHandler(this.btnMedium_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::WindowsFormsApp5.Properties.Resources.asd;
            this.ClientSize = new System.Drawing.Size(637, 450);
            this.Controls.Add(this.btnMedium);
            this.Controls.Add(this.btnHard);
            this.Controls.Add(this.btnEasy);
            this.Controls.Add(this.lblMovesLeft);
            this.Name = "Form1";
            this.Text = "Puzzle Uygulaması";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMovesLeft;
        private System.Windows.Forms.Button btnEasy;
        private System.Windows.Forms.Button btnHard;
        private System.Windows.Forms.Button btnMedium;
    }
}

