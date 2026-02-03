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
        public Form1()
        {
            InitializeComponent();
            _settingsProvider = new AlgorithmSettingsManager();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await InicijalizujAplikaciju();
        }

        private async Task InicijalizujAplikaciju()
        {
            lblStatus.Text = "Aplikacija se učitava, molimo sačekajte...";
            lblStatus.Visible = true;
            button_settings.Visible = false;
            button_settings.Enabled = false;

            try
            {
                await _settingsProvider.LoadAllSettingsAsync();

                lblStatus.Text = "Sistem spreman.";
                button_settings.Enabled = true;
                button_settings.Visible = true;

                await Task.Delay(2000);
                lblStatus.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Greška pri učitavanju: {ex.Message}", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "Greška pri učitavanju.";
            }
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
