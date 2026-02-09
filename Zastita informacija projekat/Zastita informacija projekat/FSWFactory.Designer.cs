namespace Zastita_informacija_projekat
{
    partial class FSWFactory
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtIme = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPutanja = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clbFilteri = new System.Windows.Forms.CheckedListBox();
            this.chkRekurzivno = new System.Windows.Forms.CheckBox();
            this.cmbAlgoritmi = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbTipovi = new System.Windows.Forms.ComboBox();
            this.btnKreiraj = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(295, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dodajte novi File Sysyem Watcher ovde";
            // 
            // txtIme
            // 
            this.txtIme.Location = new System.Drawing.Point(369, 81);
            this.txtIme.Name = "txtIme";
            this.txtIme.Size = new System.Drawing.Size(134, 20);
            this.txtIme.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(281, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Naziv FSW-a";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(187, 124);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Folder koji se prati";
            // 
            // txtPutanja
            // 
            this.txtPutanja.Location = new System.Drawing.Point(298, 121);
            this.txtPutanja.Name = "txtPutanja";
            this.txtPutanja.Size = new System.Drawing.Size(205, 20);
            this.txtPutanja.TabIndex = 4;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(524, 113);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(101, 34);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Izaberite folder za praćenje";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.cmbTipovi);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cmbAlgoritmi);
            this.groupBox1.Controls.Add(this.chkRekurzivno);
            this.groupBox1.Controls.Add(this.clbFilteri);
            this.groupBox1.Location = new System.Drawing.Point(114, 164);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 162);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Podešavanja";
            // 
            // clbFilteri
            // 
            this.clbFilteri.FormattingEnabled = true;
            this.clbFilteri.ImeMode = System.Windows.Forms.ImeMode.On;
            this.clbFilteri.Items.AddRange(new object[] {
            "Praćenje promena atributa",
            "Praćenje promena imena",
            "Praćenje promena veličine fajla"});
            this.clbFilteri.Location = new System.Drawing.Point(21, 83);
            this.clbFilteri.Name = "clbFilteri";
            this.clbFilteri.Size = new System.Drawing.Size(199, 49);
            this.clbFilteri.TabIndex = 0;
            // 
            // chkRekurzivno
            // 
            this.chkRekurzivno.AutoSize = true;
            this.chkRekurzivno.Location = new System.Drawing.Point(21, 34);
            this.chkRekurzivno.Name = "chkRekurzivno";
            this.chkRekurzivno.Size = new System.Drawing.Size(159, 17);
            this.chkRekurzivno.TabIndex = 1;
            this.chkRekurzivno.Text = "Rekurzivno praćenje foldera";
            this.chkRekurzivno.UseVisualStyleBackColor = true;
            // 
            // cmbAlgoritmi
            // 
            this.cmbAlgoritmi.FormattingEnabled = true;
            this.cmbAlgoritmi.Items.AddRange(new object[] {
            "Enigma",
            "XXTEA",
            "CFB"});
            this.cmbAlgoritmi.Location = new System.Drawing.Point(406, 37);
            this.cmbAlgoritmi.Name = "cmbAlgoritmi";
            this.cmbAlgoritmi.Size = new System.Drawing.Size(138, 21);
            this.cmbAlgoritmi.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(283, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Kriptofrafski algoritam";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(319, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Tipovi fajlova";
            // 
            // cmbTipovi
            // 
            this.cmbTipovi.FormattingEnabled = true;
            this.cmbTipovi.Items.AddRange(new object[] {
            "*.*",
            "*.txt",
            "*.bin"});
            this.cmbTipovi.Location = new System.Drawing.Point(406, 94);
            this.cmbTipovi.Name = "cmbTipovi";
            this.cmbTipovi.Size = new System.Drawing.Size(138, 21);
            this.cmbTipovi.TabIndex = 8;
            // 
            // btnKreiraj
            // 
            this.btnKreiraj.Location = new System.Drawing.Point(352, 375);
            this.btnKreiraj.Name = "btnKreiraj";
            this.btnKreiraj.Size = new System.Drawing.Size(101, 33);
            this.btnKreiraj.TabIndex = 10;
            this.btnKreiraj.Text = "Kreiraj FSW";
            this.btnKreiraj.UseVisualStyleBackColor = true;
            this.btnKreiraj.Click += new System.EventHandler(this.btnKreiraj_Click);
            // 
            // FSWFactory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnKreiraj);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPutanja);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtIme);
            this.Controls.Add(this.label1);
            this.Name = "FSWFactory";
            this.Text = "FSWFactory";
            this.Load += new System.EventHandler(this.FSWFactory_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIme;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPutanja;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckedListBox clbFilteri;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbAlgoritmi;
        private System.Windows.Forms.CheckBox chkRekurzivno;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbTipovi;
        private System.Windows.Forms.Button btnKreiraj;
    }
}