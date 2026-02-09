namespace Zastita_informacija_projekat
{
    partial class FSWUserControl
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnToogle = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lvOriginalni = new System.Windows.Forms.ListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listView3 = new System.Windows.Forms.ListView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(169, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Naziv:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Kriptografski algoritam:";
            // 
            // btnToogle
            // 
            this.btnToogle.Location = new System.Drawing.Point(349, 37);
            this.btnToogle.Name = "btnToogle";
            this.btnToogle.Size = new System.Drawing.Size(149, 62);
            this.btnToogle.TabIndex = 2;
            this.btnToogle.Text = "button1";
            this.btnToogle.UseVisualStyleBackColor = true;
            this.btnToogle.Click += new System.EventHandler(this.btnToogle_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(525, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(131, 59);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // lvOriginalni
            // 
            this.lvOriginalni.HideSelection = false;
            this.lvOriginalni.Location = new System.Drawing.Point(0, 19);
            this.lvOriginalni.Name = "lvOriginalni";
            this.lvOriginalni.Size = new System.Drawing.Size(668, 123);
            this.lvOriginalni.TabIndex = 4;
            this.lvOriginalni.UseCompatibleStateImageBehavior = false;
            this.lvOriginalni.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            this.lvOriginalni.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lvOriginalni);
            this.groupBox1.Location = new System.Drawing.Point(28, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(668, 142);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Praćeni folder";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.listView3);
            this.groupBox2.Location = new System.Drawing.Point(28, 282);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(668, 142);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kodirani fajlovi";
            // 
            // listView3
            // 
            this.listView3.HideSelection = false;
            this.listView3.Location = new System.Drawing.Point(0, 19);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(668, 123);
            this.listView3.TabIndex = 4;
            this.listView3.UseCompatibleStateImageBehavior = false;
            // 
            // FSWUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnToogle);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "FSWUserControl";
            this.Size = new System.Drawing.Size(727, 463);
            this.Load += new System.EventHandler(this.FSWUserControl_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnToogle;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListView lvOriginalni;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView3;
    }
}
