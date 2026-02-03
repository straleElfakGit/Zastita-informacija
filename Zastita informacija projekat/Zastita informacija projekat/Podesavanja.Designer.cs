namespace Zastita_informacija_projekat
{
    partial class Podesavanja
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnEnigma = new System.Windows.Forms.Button();
            this.btnXXTEA = new System.Windows.Forms.Button();
            this.btnCFB = new System.Windows.Forms.Button();
            this.btnTiger = new System.Windows.Forms.Button();
            this.panelContent = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.panelContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnEnigma
            // 
            this.btnEnigma.Location = new System.Drawing.Point(12, 12);
            this.btnEnigma.Name = "btnEnigma";
            this.btnEnigma.Size = new System.Drawing.Size(128, 63);
            this.btnEnigma.TabIndex = 0;
            this.btnEnigma.Text = "Enigma";
            this.btnEnigma.UseVisualStyleBackColor = true;
            this.btnEnigma.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnXXTEA
            // 
            this.btnXXTEA.Location = new System.Drawing.Point(157, 12);
            this.btnXXTEA.Name = "btnXXTEA";
            this.btnXXTEA.Size = new System.Drawing.Size(128, 63);
            this.btnXXTEA.TabIndex = 1;
            this.btnXXTEA.Text = "XXTEA";
            this.btnXXTEA.UseVisualStyleBackColor = true;
            this.btnXXTEA.Click += new System.EventHandler(this.btnXXTEA_Click);
            // 
            // btnCFB
            // 
            this.btnCFB.Location = new System.Drawing.Point(300, 12);
            this.btnCFB.Name = "btnCFB";
            this.btnCFB.Size = new System.Drawing.Size(128, 63);
            this.btnCFB.TabIndex = 2;
            this.btnCFB.Text = "CFB";
            this.btnCFB.UseVisualStyleBackColor = true;
            this.btnCFB.Click += new System.EventHandler(this.btnCFB_Click);
            // 
            // btnTiger
            // 
            this.btnTiger.Location = new System.Drawing.Point(446, 12);
            this.btnTiger.Name = "btnTiger";
            this.btnTiger.Size = new System.Drawing.Size(128, 63);
            this.btnTiger.TabIndex = 3;
            this.btnTiger.Text = "Tiger hash";
            this.btnTiger.UseVisualStyleBackColor = true;
            this.btnTiger.Click += new System.EventHandler(this.btnTiger_Click);
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.label1);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelContent.Location = new System.Drawing.Point(0, 91);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(730, 364);
            this.panelContent.TabIndex = 4;
            this.panelContent.Paint += new System.Windows.Forms.PaintEventHandler(this.panelContent_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(234, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Iazberite algoritam za koji želite da podesite parametre...";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(590, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 63);
            this.button1.TabIndex = 5;
            this.button1.Text = "Generalna podešavanja";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Podesavanja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 455);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.btnTiger);
            this.Controls.Add(this.btnCFB);
            this.Controls.Add(this.btnXXTEA);
            this.Controls.Add(this.btnEnigma);
            this.Name = "Podesavanja";
            this.Text = "Podešavanja";
            this.Load += new System.EventHandler(this.Podesavanja_Load);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnEnigma;
        private System.Windows.Forms.Button btnXXTEA;
        private System.Windows.Forms.Button btnCFB;
        private System.Windows.Forms.Button btnTiger;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
    }
}