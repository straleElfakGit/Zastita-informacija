namespace Zastita_informacija_projekat
{
    partial class SingleRotorControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbLibrary = new System.Windows.Forms.ComboBox();
            this.txtWiring = new System.Windows.Forms.TextBox();
            this.numNotch = new System.Windows.Forms.NumericUpDown();
            this.numRing = new System.Windows.Forms.NumericUpDown();
            this.numKey = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numNotch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRing)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKey)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbLibrary
            // 
            this.cbLibrary.FormattingEnabled = true;
            this.cbLibrary.Location = new System.Drawing.Point(177, 19);
            this.cbLibrary.Name = "cbLibrary";
            this.cbLibrary.Size = new System.Drawing.Size(121, 21);
            this.cbLibrary.TabIndex = 0;
            this.cbLibrary.SelectedIndexChanged += new System.EventHandler(this.cbLibrary_SelectedIndexChanged);
            // 
            // txtWiring
            // 
            this.txtWiring.Location = new System.Drawing.Point(61, 66);
            this.txtWiring.Name = "txtWiring";
            this.txtWiring.Size = new System.Drawing.Size(380, 20);
            this.txtWiring.TabIndex = 1;
            // 
            // numNotch
            // 
            this.numNotch.Location = new System.Drawing.Point(84, 105);
            this.numNotch.Name = "numNotch";
            this.numNotch.Size = new System.Drawing.Size(116, 20);
            this.numNotch.TabIndex = 2;
            // 
            // numRing
            // 
            this.numRing.Location = new System.Drawing.Point(305, 107);
            this.numRing.Name = "numRing";
            this.numRing.Size = new System.Drawing.Size(116, 20);
            this.numRing.TabIndex = 3;
            // 
            // numKey
            // 
            this.numKey.Location = new System.Drawing.Point(182, 146);
            this.numKey.Name = "numKey";
            this.numKey.Size = new System.Drawing.Size(116, 20);
            this.numKey.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(102, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Naziv rotora";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Wiring";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Notch";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(260, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Ring";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(137, 148);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Key";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(177, 190);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 54);
            this.button1.TabIndex = 10;
            this.button1.Text = "Sačuvaj u biblioteku";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numKey);
            this.groupBox1.Controls.Add(this.numRing);
            this.groupBox1.Controls.Add(this.numNotch);
            this.groupBox1.Controls.Add(this.txtWiring);
            this.groupBox1.Controls.Add(this.cbLibrary);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(465, 255);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // SingleRotorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "SingleRotorControl";
            this.Size = new System.Drawing.Size(475, 260);
            this.Load += new System.EventHandler(this.SingleRotorControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numNotch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRing)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numKey)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbLibrary;
        private System.Windows.Forms.TextBox txtWiring;
        private System.Windows.Forms.NumericUpDown numNotch;
        private System.Windows.Forms.NumericUpDown numRing;
        private System.Windows.Forms.NumericUpDown numKey;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
