using CrytoWatcher;
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
    public partial class Form1 : Form
    {
        private AlgorithmSettingsManager _settingsProvider;
        List<CryptoWatcher> mojiWatcheri;
        private Dictionary<CryptoWatcher, FSWUserControl> _watcherMap = new Dictionary<CryptoWatcher, FSWUserControl>();
        public Form1()
        {
            InitializeComponent();
            _settingsProvider = new AlgorithmSettingsManager();
            mojiWatcheri = new List<CryptoWatcher>();

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
            button1.Visible = false;
            button1.Enabled = false;
            lblNoWatchers.Visible = false;
            cmbWatchers.Visible = false;
            UIHelper.CenterInParent(lblNoWatchers);

            UIHelper.CenterInParent(listBoxLogs);
            UIHelper.CenterInParent(label1);
            UIHelper.CenterInParent(cmbWatchers);
            groupBox1.Visible = false;

            try
            {
                await _settingsProvider.LoadAllSettingsAsync();
                SetLoggerStrategy();

                lblStatus.Text = "Sistem spreman.";
                button_settings.Enabled = true;
                button_settings.Visible = true;
                button1.Visible = true;
                button1.Enabled = true;
                lblNoWatchers.Visible = true;
                cmbWatchers.Visible = true;

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

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Da li ste sigurni da želite da zatvorite aplikaciju?",
                "Potvrda izlaza",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                ZatvoriSveWatchere();
                Logger.Logger.Instance.Log($"Aplikacija zatvorena u: {DateTime.Now}", LogType.Info);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            using (var fswForm = new FSWFactory(mojiWatcheri))
            {
                if (fswForm.ShowDialog(this) == DialogResult.OK)
                {
                    var noviWatcher = mojiWatcheri.Last();
                    var novaKontrola = new FSWUserControl(noviWatcher);
                    _watcherMap.Add(noviWatcher, novaKontrola);

                    OsveziListuWatchera();
                }
            }
        }

        private void OsveziListuWatchera(int selectIndex = -1)
        {
            cmbWatchers.DataSource = null;

            if (mojiWatcheri.Count == 0)
            {
                cmbWatchers.Visible = false;
                pnlWatcherDisplay.Controls.Clear();
                lblNoWatchers.Visible = true;
            }
            else
            {
                lblNoWatchers.Visible = false;
                cmbWatchers.Visible = true;

                cmbWatchers.DataSource = mojiWatcheri;
                cmbWatchers.DisplayMember = "Name";

                cmbWatchers.SelectedIndex = (selectIndex == -1) ? mojiWatcheri.Count - 1 : selectIndex;
            }
        }

        private void cmbWatchers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbWatchers.SelectedItem is CryptoWatcher odabrani)
            {
                pnlWatcherDisplay.Controls.Clear();

                var kontrola = _watcherMap[odabrani];
                kontrola.Dock = DockStyle.Fill;

                pnlWatcherDisplay.Controls.Add(kontrola);
            }
        }

        private void ObrisiWatcher(CryptoWatcher watcher)
        {
            if (_watcherMap.ContainsKey(watcher))
            {
                var kontrola = _watcherMap[watcher];

                if (kontrola is FSWUserControl fswControl)
                {
                    fswControl.Cleanup();
                }

                kontrola.Dispose();
                _watcherMap.Remove(watcher);
            }

            mojiWatcheri.Remove(watcher);
            OsveziListuWatchera();
        }

        private void ZatvoriSveWatchere()
        {
            try
            {
                foreach (var kontrola in _watcherMap.Values)
                {
                    if (kontrola is FSWUserControl fswControl)
                    {
                        fswControl.Cleanup();
                    }
                }

                foreach (var watcher in mojiWatcheri)
                {
                    watcher.Stop();
                }

                pnlWatcherDisplay.Controls.Clear();
                _watcherMap.Clear();
                mojiWatcheri.Clear();

                OsveziListuWatchera();
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Log($"Greška pri zatvaranju watchera: {ex.Message}", LogType.Error);
            }
        }
    }
}
