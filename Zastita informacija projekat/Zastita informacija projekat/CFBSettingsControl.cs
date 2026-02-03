using PodesavanjaAlgoritama;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zastita_informacija_projekat
{
    public partial class CFBSettingsControl : UserControl, ISettingsControl
    {
        private CFBSettings _cfbSettings;
        private bool _desilaSePromena;

        public bool DesilaSePromena
        {
            get => _desilaSePromena;
            private set => _desilaSePromena = value;
        }
        public CFBSettingsControl(CFBSettings cfbSettings)
        {
            InitializeComponent();
            this._cfbSettings = cfbSettings;
            numSBits.Minimum = 8;
            numSBits.Maximum = 1024;
            numSBits.Increment = 8;

            UcitajKontrole();
        }

        private void UcitajKontrole()
        {
            txtIV.TextChanged -= TxtIV_TextChanged;
            numSBits.ValueChanged -= NumSBits_ValueChanged;

            numSBits.Value = _cfbSettings.SBits;
            txtIV.Text = BitConverter.ToString(_cfbSettings.IV).Replace("-", "");

            ValidirajIVBoju();

            _desilaSePromena = false;

            txtIV.TextChanged += TxtIV_TextChanged;
            numSBits.ValueChanged += NumSBits_ValueChanged;
        }

        private void TxtIV_TextChanged(object sender, EventArgs e)
        {
            ValidirajIVBoju();
            _desilaSePromena = true;
        }

        private void NumSBits_ValueChanged(object sender, EventArgs e)
        {
            _desilaSePromena = true;
        }

        private void ValidirajIVBoju()
        {
            bool isParnogBroja = txtIV.Text.Length % 2 == 0 && txtIV.Text.Length > 0;
            bool isHex = txtIV.Text.All(c => "0123456789ABCDEFabcdef".Contains(c));

            txtIV.BackColor = (isParnogBroja && isHex) ? Color.LightGreen : Color.MistyRose;
        }

        public void SaveSettings()
        {
            if (numSBits.Value % 8 != 0)
            {
                MessageBox.Show("Parametar s mora biti deljiv sa 8!");
                return;
            }

            try
            {
                _cfbSettings.SBits = (int)numSBits.Value;
                _cfbSettings.IV = HexStringToByteArray(txtIV.Text);

                CFBSettingsManager.Instance.Save(_cfbSettings);
                _desilaSePromena = false;
                MessageBox.Show("CFB podešavanja su uspešno sačuvana!");
            }
            catch
            {
                MessageBox.Show("IV mora biti validan Hex string parne dužine!");
            }
        }

        private byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }



        private void CFBSettingsControl_Load(object sender, EventArgs e)
        {
            UIHelper.CenterInParent(label1);
            UIHelper.CenterInParent(label2);
        }

        private void btnReset_Click(object sender, EventArgs e) => UcitajKontrole();
        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            byte[] b = new byte[8];
            rnd.NextBytes(b);

            txtIV.Text = BitConverter.ToString(b).Replace("-", "");
            _desilaSePromena = true;
        }

        private void btnSave_Click(object sender, EventArgs e) => SaveSettings();
    }
}
