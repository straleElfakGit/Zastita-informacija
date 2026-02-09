namespace Zastita_informacija_projekat
{
    partial class Form1
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
            this.button_settings = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.listBoxLogs = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlWatcherDisplay = new System.Windows.Forms.Panel();
            this.lblNoWatchers = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbWatchers = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.pnlWatcherDisplay.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_settings
            // 
            this.button_settings.Location = new System.Drawing.Point(574, 53);
            this.button_settings.Name = "button_settings";
            this.button_settings.Size = new System.Drawing.Size(138, 65);
            this.button_settings.TabIndex = 0;
            this.button_settings.Text = "Podešavanja";
            this.button_settings.UseVisualStyleBackColor = true;
            this.button_settings.Click += new System.EventHandler(this.button1_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(571, 135);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "label1";
            // 
            // listBoxLogs
            // 
            this.listBoxLogs.FormattingEnabled = true;
            this.listBoxLogs.Location = new System.Drawing.Point(19, 71);
            this.listBoxLogs.Name = "listBoxLogs";
            this.listBoxLogs.Size = new System.Drawing.Size(316, 589);
            this.listBoxLogs.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(115, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Aktivnosti u aplikaciji";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.listBoxLogs);
            this.groupBox1.Location = new System.Drawing.Point(25, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(355, 681);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lista aktivnosti";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(805, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(138, 65);
            this.button1.TabIndex = 5;
            this.button1.Text = "Dodaj novi File System Watcher";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // pnlWatcherDisplay
            // 
            this.pnlWatcherDisplay.Controls.Add(this.lblNoWatchers);
            this.pnlWatcherDisplay.Location = new System.Drawing.Point(8, 66);
            this.pnlWatcherDisplay.Name = "pnlWatcherDisplay";
            this.pnlWatcherDisplay.Size = new System.Drawing.Size(727, 463);
            this.pnlWatcherDisplay.TabIndex = 6;
            // 
            // lblNoWatchers
            // 
            this.lblNoWatchers.AutoSize = true;
            this.lblNoWatchers.Location = new System.Drawing.Point(353, 216);
            this.lblNoWatchers.Name = "lblNoWatchers";
            this.lblNoWatchers.Size = new System.Drawing.Size(556, 13);
            this.lblNoWatchers.TabIndex = 7;
            this.lblNoWatchers.Text = "Trenutni nema ni jednog aktivnog File System Watcher-a. Napravite novi FSW kako b" +
    "i ste omogućili praćenje fajlova";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cmbWatchers);
            this.groupBox2.Controls.Add(this.pnlWatcherDisplay);
            this.groupBox2.Location = new System.Drawing.Point(397, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(743, 539);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lista FSW-ova";
            // 
            // cmbWatchers
            // 
            this.cmbWatchers.FormattingEnabled = true;
            this.cmbWatchers.Location = new System.Drawing.Point(300, 30);
            this.cmbWatchers.Name = "cmbWatchers";
            this.cmbWatchers.Size = new System.Drawing.Size(121, 21);
            this.cmbWatchers.TabIndex = 8;
            this.cmbWatchers.SelectedIndexChanged += new System.EventHandler(this.cmbWatchers_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1155, 721);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.button_settings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlWatcherDisplay.ResumeLayout(false);
            this.pnlWatcherDisplay.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_settings;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListBox listBoxLogs;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel pnlWatcherDisplay;
        private System.Windows.Forms.Label lblNoWatchers;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cmbWatchers;
    }
}

