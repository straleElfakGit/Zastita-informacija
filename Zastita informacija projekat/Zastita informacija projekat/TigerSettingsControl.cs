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
    public partial class TigerSettingsControl : UserControl, ISettingsControl
    {
        private TigerHashSettings _tigerSettings;
        bool _desilaSePromena;
        public event EventHandler OnSettingsChanged;
        public bool DesilaSePromena
        {
            get => _desilaSePromena;
            private set
            {
                _desilaSePromena = value;
                OnSettingsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public TigerSettingsControl(TigerHashSettings tigerHashSettings)
        {
            InitializeComponent();
            _tigerSettings = tigerHashSettings;
            UcitajKontrole();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void TigerSettingsControl_Load(object sender, EventArgs e)
        {
            label1.Text = "Izaberite način na koji se ostvaruje padding kod Tiger hash-a";
            UIHelper.CenterInParent(label1);
            UIHelper.CenterInParent(radioButton1);
            UIHelper.CenterInParent(radioButton2);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _desilaSePromena = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _desilaSePromena = true;
        }

        private void UcitajKontrole()
        {
            radioButton1.CheckedChanged -= RadioButton_CheckedChanged;
            radioButton2.CheckedChanged -= RadioButton_CheckedChanged;

            switch (_tigerSettings.SelectedStrategy)
            {
                case PaddingStrategy.SimpleZeroPadding: radioButton1.Checked = true; break;
                case PaddingStrategy.StandardMerkleDamgard: radioButton2.Checked = true; break;
            }

            DesilaSePromena = false;

            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton_CheckedChanged;
        }

        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (((RadioButton)sender).Checked)
                DesilaSePromena = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UcitajKontrole();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SaveSettings();
        }

        public void SaveSettings()
        {
            if (radioButton1.Checked) _tigerSettings.SelectedStrategy = PaddingStrategy.SimpleZeroPadding;
            else _tigerSettings.SelectedStrategy = PaddingStrategy.StandardMerkleDamgard;

            TigerHashSettingsManager.Instance.Save(_tigerSettings);
            DesilaSePromena = false;
            Logger.Logger.Instance.Log("Uspešno sačuvanja podešavanja za Tiger hash.", LogType.Info);

            MessageBox.Show("Podešavanja za TigerHash su sačuvana.","Sačuvane promene", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
