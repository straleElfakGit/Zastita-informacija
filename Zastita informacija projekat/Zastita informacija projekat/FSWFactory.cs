using CrytoWatcher;
using PodesavanjaAlgoritama;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using AlgorithmSettingsManagerr;

namespace Zastita_informacija_projekat
{
    public partial class FSWFactory : Form
    {
        private List<CryptoWatcher> _activeWatchers;
        private readonly string[] TextExtensions = {
            ".txt", ".xml", ".json", ".csv", ".rtf",  
            ".c", ".cs", ".cpp", ".h", ".java",       
            ".py", ".js", ".html", ".css", ".sql",   
            ".ini", ".cfg", ".config", ".yaml", ".yml", 
            ".log", ".md", ".bat", ".sh" };
        private AlgorithmSettingsManager _settingsManager;
        public FSWFactory(List<CryptoWatcher> list, AlgorithmSettingsManager settingsManager)
        {
            InitializeComponent();
            _activeWatchers = list;
            this._settingsManager = settingsManager;
            UcitajKontrole();
        }

        private void UcitajKontrole()
        {
            cmbAlgoritmi.DataSource = Enum.GetValues(typeof(CryptoAlgorithm));

            List<string> sviTipovi = new List<string> { "*.*", "*.bin", "*.dat" };

            foreach (string te in TextExtensions)
            {
                sviTipovi.Add("*" + te);
            }
            cmbTipovi.DataSource = sviTipovi;
            cmbTipovi.SelectedIndex = 0;
        }



        private void FSWFactory_Load(object sender, EventArgs e)
        {
            UIHelper.CenterInParent(label1);
            UIHelper.CenterInParent(groupBox1);
            UIHelper.CenterInParent(btnKreiraj);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.SelectedPath = @"C:\Podaci\Fakultet\Semestar_VII\Zastita_informacija\Projakat\Zastita-informacija";

                fbd.Description = "Izaberite folder koji će ovaj FSW pratiti";
                fbd.ShowNewFolderButton = true;
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    txtPutanja.Text = fbd.SelectedPath;
                }
            }
        }

        private void btnKreiraj_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPutanja.Text))
            {
                Logger.Logger.Instance.Log("Kreiranje FSW-a nije uspelo: Morate izabrati folder!", Logger.LogType.Error);
                MessageBox.Show("Morate izabrati folder!");
                return;
            }

            string fswName = string.IsNullOrWhiteSpace(txtIme.Text)
                ? $"FSW_{_activeWatchers.Count + 1}"
                : txtIme.Text;

            CryptoAlgorithm selectedAlgo = (CryptoAlgorithm)cmbAlgoritmi.SelectedItem;
            string filter = cmbTipovi.Text;

            if (selectedAlgo == CryptoAlgorithm.Enigma && !IsTextBasedFilter(filter))
            {
                MessageBox.Show("Enigma zahteva tekstualni filter (npr. *.txt, *.json, *.cs).");
                Logger.Logger.Instance.Log("Kreiranje FSW-a nije uspelo: Enigma zahteva tekstualni filter (npr. *.txt, *.json, *.cs).", Logger.LogType.Error);
                return;
            }

            try
            {
                var cw = new CryptoWatcher(fswName, txtPutanja.Text, selectedAlgo, _settingsManager);

                cw.Watcher.IncludeSubdirectories = chkRekurzivno.Checked;
                cw.Watcher.Filter = filter;

                NotifyFilters filters = GetSelectedFilters();
                if (filters == 0)
                {
                    filters = NotifyFilters.FileName | NotifyFilters.CreationTime;
                }

            
                if ((filters & NotifyFilters.Size) != 0)
                {
                    filters |= NotifyFilters.LastWrite;
                }

               
                if ((filters & NotifyFilters.Attributes) != 0)
                {
                    filters |= NotifyFilters.LastWrite;
                }

                cw.Start();

                _activeWatchers.Add(cw);
                MessageBox.Show($"FSW '{fswName}' je uspešno kreiran i pokrenut!");
                Logger.Logger.Instance.Log($"FSW '{fswName}' je uspešno kreiran i pokrenut!", Logger.LogType.Info);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri kreiranju: {ex.Message}");
                Logger.Logger.Instance.Log($"Kreiranje FSW-a nije uspelo: Greška pri kreiranju: {ex.Message}", Logger.LogType.Error);
            }
        }

        private NotifyFilters GetSelectedFilters()
        {
            NotifyFilters combinedFilters = 0;

            foreach (var item in clbFilteri.CheckedItems)
            {
                string text = item.ToString();
                if (text.Contains("atributa")) combinedFilters |= NotifyFilters.Attributes;
                if (text.Contains("imena")) combinedFilters |= NotifyFilters.FileName;
                if (text.Contains("veličine")) combinedFilters |= NotifyFilters.Size;
            }

            return combinedFilters == 0 ? NotifyFilters.FileName : combinedFilters;
        }

        private bool IsTextBasedFilter(string filter)
        {
            if (filter == "*.*") return false;
            string ext = Path.GetExtension(filter).ToLower();
            return TextExtensions.Contains(ext);
        }
    }
}
