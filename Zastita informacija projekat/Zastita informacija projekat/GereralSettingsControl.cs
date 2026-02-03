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
    public partial class GereralSettingsControl : UserControl, ISettingsControl
    {
        private GeneralSettings _generalSettings;
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
        public GereralSettingsControl(GeneralSettings generalSettings)
        {
            InitializeComponent();
            _generalSettings = generalSettings;
            UcitajKontrole();
        }

        private void GereralSettingsControl_Load(object sender, EventArgs e)
        {
            UIHelper.CenterInParent(label1);
            UIHelper.CenterInParent(radioButton1);
            UIHelper.CenterInParent(radioButton2);
            UIHelper.CenterInParent(radioButton3);
            UIHelper.CenterInParent(radioButton4);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            _desilaSePromena = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            _desilaSePromena = true;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            _desilaSePromena = true;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            _desilaSePromena = true;
        }
        private void UcitajKontrole()
        {
            radioButton1.CheckedChanged -= RadioButton_CheckedChanged;
            radioButton2.CheckedChanged -= RadioButton_CheckedChanged;
            radioButton3.CheckedChanged -= RadioButton_CheckedChanged;
            radioButton4.CheckedChanged -= RadioButton_CheckedChanged;

            switch (_generalSettings.LoggingFilesFrequence)
            {
                case LoggingFiles.Daily: radioButton1.Checked = true; break;
                case LoggingFiles.Weekly: radioButton2.Checked = true; break;
                case LoggingFiles.Monthly: radioButton3.Checked = true; break;
                case LoggingFiles.OnlyOneFile: radioButton4.Checked = true; break;
            }

            DesilaSePromena = false;

            radioButton1.CheckedChanged += RadioButton_CheckedChanged;
            radioButton2.CheckedChanged += RadioButton_CheckedChanged;
            radioButton3.CheckedChanged += RadioButton_CheckedChanged;
            radioButton4.CheckedChanged += RadioButton_CheckedChanged;
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
            if (radioButton1.Checked)
            {
                _generalSettings.LoggingFilesFrequence = LoggingFiles.Daily;
                Logger.Logger.Instance.SetStrategy(new DailyLogStrategy());
            }
            else if (radioButton2.Checked)
            {
                _generalSettings.LoggingFilesFrequence = LoggingFiles.Weekly;
                Logger.Logger.Instance.SetStrategy(new WeeklyLogStrategy());
            }
            else if (radioButton3.Checked)
            {
                _generalSettings.LoggingFilesFrequence = LoggingFiles.Monthly;
                Logger.Logger.Instance.SetStrategy(new MonthlyLogStrategy());
            }
            else if (radioButton4.Checked)
            {
                _generalSettings.LoggingFilesFrequence = LoggingFiles.OnlyOneFile;
                Logger.Logger.Instance.SetStrategy(new GeneralLogStrategy());
            }

            Logger.Logger.Instance.Log("Način logovanja aktivnosti je uspešno promenjen.", LogType.Info);

            GeneralSettingsManager.Instance.Save(_generalSettings);
            DesilaSePromena = false;
            MessageBox.Show("Podešavanja za učestanost kreiranja fajlova su sačuvana.", "Sačuvane promene", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
