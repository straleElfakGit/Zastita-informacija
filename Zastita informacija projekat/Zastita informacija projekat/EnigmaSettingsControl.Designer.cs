namespace Zastita_informacija_projekat
{
    partial class EnigmaSettingsControl
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReflector = new System.Windows.Forms.TextBox();
            this.txtPlugboard = new System.Windows.Forms.TextBox();
            this.numTotalRotors = new System.Windows.Forms.NumericUpDown();
            this.numCurrentIndex = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panelActiveRotor = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.cbReflectorLibrary = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numBlockSize = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.numTotalRotors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlockSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reflektor";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(481, 245);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "PlugBoard";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtReflector
            // 
            this.txtReflector.Location = new System.Drawing.Point(58, 72);
            this.txtReflector.Name = "txtReflector";
            this.txtReflector.Size = new System.Drawing.Size(148, 20);
            this.txtReflector.TabIndex = 3;
            this.txtReflector.TextChanged += new System.EventHandler(this.txtReflector_TextChanged);
            // 
            // txtPlugboard
            // 
            this.txtPlugboard.Location = new System.Drawing.Point(543, 242);
            this.txtPlugboard.Name = "txtPlugboard";
            this.txtPlugboard.Size = new System.Drawing.Size(148, 20);
            this.txtPlugboard.TabIndex = 4;
            this.txtPlugboard.TextChanged += new System.EventHandler(this.txtPlugboard_TextChanged);
            // 
            // numTotalRotors
            // 
            this.numTotalRotors.Location = new System.Drawing.Point(351, 20);
            this.numTotalRotors.Name = "numTotalRotors";
            this.numTotalRotors.Size = new System.Drawing.Size(120, 20);
            this.numTotalRotors.TabIndex = 5;
            this.numTotalRotors.ValueChanged += new System.EventHandler(this.numTotalRotors_ValueChanged);
            // 
            // numCurrentIndex
            // 
            this.numCurrentIndex.Location = new System.Drawing.Point(587, 20);
            this.numCurrentIndex.Name = "numCurrentIndex";
            this.numCurrentIndex.Size = new System.Drawing.Size(120, 20);
            this.numCurrentIndex.TabIndex = 6;
            this.numCurrentIndex.ValueChanged += new System.EventHandler(this.numCurrentIndex_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(250, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ukupan broj rotora";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(501, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Trenutni indeks";
            // 
            // panelActiveRotor
            // 
            this.panelActiveRotor.Location = new System.Drawing.Point(18, 64);
            this.panelActiveRotor.Name = "panelActiveRotor";
            this.panelActiveRotor.Size = new System.Drawing.Size(403, 277);
            this.panelActiveRotor.TabIndex = 9;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(58, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 32);
            this.button1.TabIndex = 4;
            this.button1.Text = "Dodaj u biblioteku";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(460, 296);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(104, 45);
            this.button2.TabIndex = 11;
            this.button2.Text = "Sačuvaj promene";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(603, 296);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(104, 45);
            this.button3.TabIndex = 12;
            this.button3.Text = "Resetuj stanje";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cbReflectorLibrary
            // 
            this.cbReflectorLibrary.FormattingEnabled = true;
            this.cbReflectorLibrary.Location = new System.Drawing.Point(58, 29);
            this.cbReflectorLibrary.Name = "cbReflectorLibrary";
            this.cbReflectorLibrary.Size = new System.Drawing.Size(148, 21);
            this.cbReflectorLibrary.TabIndex = 13;
            this.cbReflectorLibrary.SelectedIndexChanged += new System.EventHandler(this.cbReflectorLibrary_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Naziv";
            // 
            // numBlockSize
            // 
            this.numBlockSize.Location = new System.Drawing.Point(95, 20);
            this.numBlockSize.Name = "numBlockSize";
            this.numBlockSize.Size = new System.Drawing.Size(120, 20);
            this.numBlockSize.TabIndex = 13;
            this.numBlockSize.ValueChanged += new System.EventHandler(this.numBlockSize_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Veličina rotora";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbReflectorLibrary);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.txtReflector);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(475, 64);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 158);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Podešavanje reflektora";
            // 
            // EnigmaSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.numBlockSize);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.panelActiveRotor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numCurrentIndex);
            this.Controls.Add(this.numTotalRotors);
            this.Controls.Add(this.txtPlugboard);
            this.Controls.Add(this.label3);
            this.Name = "EnigmaSettingsControl";
            this.Size = new System.Drawing.Size(730, 364);
            this.Load += new System.EventHandler(this.EnigmaSettingsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTotalRotors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBlockSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReflector;
        private System.Windows.Forms.TextBox txtPlugboard;
        private System.Windows.Forms.NumericUpDown numTotalRotors;
        private System.Windows.Forms.NumericUpDown numCurrentIndex;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelActiveRotor;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbReflectorLibrary;
        private System.Windows.Forms.NumericUpDown numBlockSize;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
