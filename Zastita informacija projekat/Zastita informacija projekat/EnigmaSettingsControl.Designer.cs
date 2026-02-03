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
            ((System.ComponentModel.ISupportInitialize)(this.numTotalRotors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentIndex)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(86, 332);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Reflektro";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(426, 332);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "PlugBoard";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txtReflector
            // 
            this.txtReflector.Location = new System.Drawing.Point(142, 329);
            this.txtReflector.Name = "txtReflector";
            this.txtReflector.Size = new System.Drawing.Size(148, 20);
            this.txtReflector.TabIndex = 3;
            this.txtReflector.TextChanged += new System.EventHandler(this.txtReflector_TextChanged);
            // 
            // txtPlugboard
            // 
            this.txtPlugboard.Location = new System.Drawing.Point(495, 325);
            this.txtPlugboard.Name = "txtPlugboard";
            this.txtPlugboard.Size = new System.Drawing.Size(148, 20);
            this.txtPlugboard.TabIndex = 4;
            this.txtPlugboard.TextChanged += new System.EventHandler(this.txtPlugboard_TextChanged);
            // 
            // numTotalRotors
            // 
            this.numTotalRotors.Location = new System.Drawing.Point(187, 21);
            this.numTotalRotors.Name = "numTotalRotors";
            this.numTotalRotors.Size = new System.Drawing.Size(120, 20);
            this.numTotalRotors.TabIndex = 5;
            this.numTotalRotors.ValueChanged += new System.EventHandler(this.numTotalRotors_ValueChanged);
            // 
            // numCurrentIndex
            // 
            this.numCurrentIndex.Location = new System.Drawing.Point(534, 21);
            this.numCurrentIndex.Name = "numCurrentIndex";
            this.numCurrentIndex.Size = new System.Drawing.Size(120, 20);
            this.numCurrentIndex.TabIndex = 6;
            this.numCurrentIndex.ValueChanged += new System.EventHandler(this.numCurrentIndex_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(74, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Ukupan broj rotora";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(437, 23);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Trenutni indeks";
            // 
            // panelActiveRotor
            // 
            this.panelActiveRotor.Location = new System.Drawing.Point(129, 59);
            this.panelActiveRotor.Name = "panelActiveRotor";
            this.panelActiveRotor.Size = new System.Drawing.Size(475, 260);
            this.panelActiveRotor.TabIndex = 9;
            // 
            // EnigmaSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelActiveRotor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.numCurrentIndex);
            this.Controls.Add(this.numTotalRotors);
            this.Controls.Add(this.txtPlugboard);
            this.Controls.Add(this.txtReflector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "EnigmaSettingsControl";
            this.Size = new System.Drawing.Size(730, 364);
            this.Load += new System.EventHandler(this.EnigmaSettingsControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numTotalRotors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCurrentIndex)).EndInit();
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
    }
}
