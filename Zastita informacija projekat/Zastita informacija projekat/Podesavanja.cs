using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zastita_informacija_projekat
{
    public partial class Podesavanja : Form
    {
        private AlgorithmSettingsManager _settingsProvider;
        private ISettingsControl _trenutnaKontrola;
        public Podesavanja(AlgorithmSettingsManager settingsProvider)
        {
            InitializeComponent();
            _settingsProvider = settingsProvider;
            this.FormClosing += ProveriPromenePreZatvaranja;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!DozvoliPromenuKontrole()) 
                return;
            var uControl = new EnigmaSettingsControl(_settingsProvider.Enigma, _settingsProvider.Library);
            ShowInPanel(uControl);
        }

        private void btnTiger_Click(object sender, EventArgs e)
        {
            if (!DozvoliPromenuKontrole()) 
                return;
            var uControl = new TigerSettingsControl(_settingsProvider.Tiger);
            ShowInPanel(uControl);
        }

        private void btnXXTEA_Click(object sender, EventArgs e)
        {
            if (!DozvoliPromenuKontrole()) 
                return;
            var uControl = new XXTEASettingsControl(_settingsProvider.XXTEA);
            ShowInPanel(uControl);
        }

        private void btnCFB_Click(object sender, EventArgs e)
        {
            if (!DozvoliPromenuKontrole()) 
                return;
            var uControl = new CFBSettingsControl(_settingsProvider.CFB);
            ShowInPanel(uControl);
        }

        private void ShowInPanel(UserControl control)
        {
            foreach (Control c in panelContent.Controls)
                c.Dispose();
            panelContent.Controls.Clear();
            control.Dock = DockStyle.Fill;
            panelContent.Controls.Add(control);
            _trenutnaKontrola = control as ISettingsControl;
        }

        private void panelContent_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Podesavanja_Load(object sender, EventArgs e)
        {
            UIHelper.CenterInParent(label1);
        }

        private bool DozvoliPromenuKontrole()
        {
            if (_trenutnaKontrola != null && _trenutnaKontrola.DesilaSePromena)
            {
                var result = MessageBox.Show(
                    "Imate nesačuvane promene. Želite li da ih sačuvate pre nego što pređete dalje?",
                    "Nesačuvane promene",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _trenutnaKontrola.SaveSettings();
                    return true;
                }
                else if (result == DialogResult.No)
                {
                    return true;
                }
                else
                {
                    return false; 
                }
            }
            return true;
        }

        private void ProveriPromenePreZatvaranja(object sender, FormClosingEventArgs e)
        {
            if (!DozvoliPromenuKontrole())
            {
                e.Cancel = true;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!DozvoliPromenuKontrole())
                return;
            var uControl = new GereralSettingsControl(_settingsProvider.General);
            ShowInPanel(uControl);
        }
    }
}