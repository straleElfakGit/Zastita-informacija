using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Logger;
using PodesavanjaAlgoritama;

namespace Zastita_informacija_projekat
{
    public partial class Form1 : Form
    {
        private AlgorithmSettingsManager _settingsProvider;
        public Form1()
        {
            InitializeComponent();
            _settingsProvider = new AlgorithmSettingsManager();

            Logger.Logger.Instance.OnLogAdded += AppendToLogBox;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await InicijalizujAplikaciju();
        }

        private void AppendToLogBox(string message)
        { 
            if (listBoxLogs.InvokeRequired)
                listBoxLogs.Invoke(new Action(() => listBoxLogs.Items.Add(message)));
            else
            {
                listBoxLogs.Items.Add(message);
                if (listBoxLogs.Items.Count > 200)
                    listBoxLogs.Items.RemoveAt(0);
                listBoxLogs.SelectedIndex = listBoxLogs.Items.Count - 1;
            }
        }

        private async Task InicijalizujAplikaciju()
        {
            Logger.Logger.Instance.Log("Pokretanje inicijalizacije aplikacije...", LogType.Info);

            lblStatus.Text = "Aplikacija se učitava, molimo sačekajte...";
            lblStatus.Visible = true;
            button_settings.Visible = false;
            button_settings.Enabled = false;

            UIHelper.CenterInParent(listBoxLogs);
            UIHelper.CenterInParent(label1);
            groupBox1.Visible = false;

            try
            {
                await _settingsProvider.LoadAllSettingsAsync();
                SetLoggerStrategy();

                lblStatus.Text = "Sistem spreman.";
                button_settings.Enabled = true;
                button_settings.Visible = true;

                await Task.Delay(2000);
                lblStatus.Visible = false;
                groupBox1.Visible = true;
                Logger.Logger.Instance.Log("Sistem uspešno učitan.", LogType.Info);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Greška pri učitavanju.";
                Logger.Logger.Instance.Log($"Kritična greška: {ex.Message}", LogType.Error);
            }
        }

        private void SetLoggerStrategy()
        {
            if (_settingsProvider.General.LoggingFilesFrequence == LoggingFiles.Daily)
                Logger.Logger.Instance.SetStrategy(new DailyLogStrategy());
            else if (_settingsProvider.General.LoggingFilesFrequence == LoggingFiles.Weekly)
                Logger.Logger.Instance.SetStrategy(new WeeklyLogStrategy());
            else if (_settingsProvider.General.LoggingFilesFrequence == LoggingFiles.Monthly)
                Logger.Logger.Instance.SetStrategy(new MonthlyLogStrategy());
            else if (_settingsProvider.General.LoggingFilesFrequence == LoggingFiles.OnlyOneFile)
                Logger.Logger.Instance.SetStrategy(new GeneralLogStrategy());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var settingsForm = new Podesavanja(_settingsProvider))
            {
                settingsForm.ShowDialog(this);
            }
        }
    }
}
