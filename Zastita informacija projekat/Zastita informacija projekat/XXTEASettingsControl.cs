using Logger;
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
    public partial class XXTEASettingsControl : UserControl, ISettingsControl
    {
        private XXTEASettings _xxteaSettings;
        private bool _desilaSePromena;
        public bool DesilaSePromena
        {
            get => _desilaSePromena;
            private set => _desilaSePromena = value;
        }
        public XXTEASettingsControl(XXTEASettings xxteaSettings)
        {
            InitializeComponent();
            this._xxteaSettings = xxteaSettings;

            OgraniciTextBoxove();

            numWordsPerBlock.Minimum = 2;
            numWordsPerBlock.Maximum = 1024;

            UcitajKontrole();
        }

        private void OgraniciTextBoxove()
        {
            var kljucevi = new[] { txtKey1, txtKey2, txtKey3, txtKey4 };
            foreach (var txt in kljucevi)
            {
                txt.MaxLength = 8;
                txt.KeyPress += TextBoxHex_KeyPress;
                txt.TextChanged += TextBoxHex_TextChanged;
            }
        }

        private void TextBoxHex_KeyPress(object sender, KeyPressEventArgs e)
        {
            char c = char.ToUpper(e.KeyChar);
            if (!char.IsControl(c) && !((c >= '0' && c <= '9') || (c >= 'A' && c <= 'F')))
            {
                e.Handled = true;
            }
        }

        private void TextBoxHex_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            bool isOk = uint.TryParse(txt.Text, System.Globalization.NumberStyles.HexNumber, null, out _) && txt.Text.Length > 0;
            txt.BackColor = isOk ? Color.LightGreen : Color.MistyRose;

            _desilaSePromena = true;
        }

        private void UcitajKontrole()
        {
            txtKey1.TextChanged -= TextBoxHex_TextChanged;
            txtKey2.TextChanged -= TextBoxHex_TextChanged;
            txtKey3.TextChanged -= TextBoxHex_TextChanged;
            txtKey4.TextChanged -= TextBoxHex_TextChanged;
            numWordsPerBlock.ValueChanged -= NumWords_ValueChanged;

            txtKey1.Text = _xxteaSettings.Key[0].ToString("X8");
            txtKey2.Text = _xxteaSettings.Key[1].ToString("X8");
            txtKey3.Text = _xxteaSettings.Key[2].ToString("X8");
            txtKey4.Text = _xxteaSettings.Key[3].ToString("X8");
            numWordsPerBlock.Value = _xxteaSettings.BrojReciPoBloku;

            AzurirajBojeBezOkidanjaFlaga();
            _desilaSePromena = false;

            txtKey1.TextChanged += TextBoxHex_TextChanged;
            txtKey2.TextChanged += TextBoxHex_TextChanged;
            txtKey3.TextChanged += TextBoxHex_TextChanged;
            txtKey4.TextChanged += TextBoxHex_TextChanged;
            numWordsPerBlock.ValueChanged += NumWords_ValueChanged;
        }

        private void AzurirajBojeBezOkidanjaFlaga()
        {
            foreach (var txt in new[] { txtKey1, txtKey2, txtKey3, txtKey4 })
            {
                bool isOk = uint.TryParse(txt.Text, System.Globalization.NumberStyles.HexNumber, null, out _) && txt.Text.Length > 0;
                txt.BackColor = isOk ? Color.LightGreen : Color.MistyRose;
            }
        }

        private void NumWords_ValueChanged(object sender, EventArgs e) => _desilaSePromena = true;

        private void XXTEASettingsControl_Load(object sender, EventArgs e)
        {
            UIHelper.CenterInParent(label1);
        }

        public void SaveSettings()
        {
            try
            {   
                if(txtKey1.Text.Length == 0 || txtKey2.Text.Length == 0 || txtKey3.Text.Length == 0 || txtKey4.Text.Length == 0)
                {
                    MessageBox.Show("Morate popuniti sva polja za ključ",
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.Logger.Instance.Log("Čuvanje podešavanja za XXTEA algoritam nije uspelo: Morate popuniti sva polja za ključ", LogType.Error);
                    return;
                }
                if ((int)numWordsPerBlock.Value < 2)
                {
                    MessageBox.Show("Broj reči po bloku mora biti veći ili jednak 2.",
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Logger.Logger.Instance.Log("Čuvanje podešavanja za XXTEA algoritam nije uspelo: Broj reči po bloku mora biti veći ili jednak 2.", LogType.Error);
                    return;
                }
                uint k1 = Convert.ToUInt32(txtKey1.Text, 16);
                uint k2 = Convert.ToUInt32(txtKey2.Text, 16);
                uint k3 = Convert.ToUInt32(txtKey3.Text, 16);
                uint k4 = Convert.ToUInt32(txtKey4.Text, 16);

                _xxteaSettings.Key = new uint[] { k1, k2, k3, k4 };
                _xxteaSettings.BrojReciPoBloku = (int)numWordsPerBlock.Value;

                if (_xxteaSettings.ConsistantSettings())
                {
                    XXTEASettingsManager.Instance.Save(_xxteaSettings);
                    _desilaSePromena = false;
                    Logger.Logger.Instance.Log("Uspešno su sačuvana nova podešvanja za XXTEA algoritam", LogType.Info);
                    MessageBox.Show("XXTEA podešavanja su uspešno sačuvana!", "Sačuvane promene", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                MessageBox.Show("Neispravan format ključa! Unesite validne Hex vrednosti (8 karaktera).",
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Logger.Instance.Log("Čuvanje podešavanja za XXTEA algoritam nije uspelo: Neispravna vrednost ključa", LogType.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e) => UcitajKontrole();

        private void btnSave_Click(object sender, EventArgs e) => SaveSettings();

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            txtKey1.Text = ((uint)rnd.Next()).ToString("X8");
            txtKey2.Text = ((uint)rnd.Next()).ToString("X8");
            txtKey3.Text = ((uint)rnd.Next()).ToString("X8");
            txtKey4.Text = ((uint)rnd.Next()).ToString("X8");

            _desilaSePromena = true;
        }
    }
}
