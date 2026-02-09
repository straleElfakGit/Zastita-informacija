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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Zastita_informacija_projekat
{
    public partial class SingleRotorControl : UserControl
    {
        private EnigmaLibrary _library;
        private int _blockSize;

        public int[] Wiring => EnigmaUtils.StringToIntArray(txtWiring.Text);
        public int Notch => (int)numNotch.Value;
        public int RingSetting => (int)numRing.Value;
        public int KeySetting => (int)numKey.Value;

        public event EventHandler PodaciIzmenjeni;
        public SingleRotorControl(EnigmaLibrary library, int redniBroj, int blockSize)
        {
            InitializeComponent();
            _library = library;
            groupBox1.Text = $"Detalji rotora na poziciji {redniBroj + 1}";
            _blockSize = blockSize;

            PopuniBiblioteku();
            ZakaciDogadjaje();
        }

        public void UcitajPodatke(int[] wiring, int notch, int ring, int key)
        {
            OtkaciDogadjaje();

            txtWiring.Text = EnigmaUtils.IntArrayToString(wiring);
            numNotch.Value = notch;
            numRing.Value = ring;
            numKey.Value = key;

            ValidirajDuzinu();

            ZakaciDogadjaje();
        }

        private void PopuniBiblioteku()
        {
            cbLibrary.Items.Clear();
            cbLibrary.Items.Add("Custom / Ručni unos");
            foreach (var r in _library.Rotors)
            {
                cbLibrary.Items.Add(r);
            }
            cbLibrary.DisplayMember = "Name";
            cbLibrary.SelectedIndex = 0;
        }

        private void OtkaciDogadjaje()
        {
            txtWiring.TextChanged -= GenericChanged;
            numNotch.ValueChanged -= GenericChanged;
            numRing.ValueChanged -= GenericChanged;
            numKey.ValueChanged -= GenericChanged;
        }

        private void ZakaciDogadjaje()
        {
            txtWiring.TextChanged += GenericChanged;
            numNotch.ValueChanged += GenericChanged;
            numRing.ValueChanged += GenericChanged;
            numKey.ValueChanged += GenericChanged;
        }

        private void GenericChanged(object sender, EventArgs e)
        {
            if (sender == txtWiring)
                ValidirajDuzinu();

            if (cbLibrary.SelectedIndex != -1)
            {
                
                cbLibrary.SelectedIndexChanged -= cbLibrary_SelectedIndexChanged;

                cbLibrary.SelectedIndex = -1;
                cbLibrary.Text = "Novi Rotor ";

                cbLibrary.SelectedIndexChanged += cbLibrary_SelectedIndexChanged;
            }

            PodaciIzmenjeni?.Invoke(this, EventArgs.Empty);
        }

        private void SingleRotorControl_Load(object sender, EventArgs e)
        {
            cbLibrary.DropDownStyle = ComboBoxStyle.DropDown;
            UIHelper.CenterInParent(label7);
        }

        private void cbLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbLibrary.SelectedItem is StandardRotor r)
            {
                OtkaciDogadjaje();
                txtWiring.Text = EnigmaUtils.IntArrayToString(r.Wiring);
                numNotch.Value = r.Notch;
                ValidirajDuzinu();
                ZakaciDogadjaje();
                PodaciIzmenjeni?.Invoke(this, EventArgs.Empty);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string porukaGreske;
            if ((porukaGreske = ProveriSveParametre(out string greska)) != null) 
            {
                MessageBox.Show(greska, "Nevalidan rotor: " + porukaGreske, MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Logger.Instance.Log("Nije uspelo dodavanje novog rotora: " + porukaGreske, Logger.LogType.Error);
                return;
            }

            string imeRotora = cbLibrary.Text;

            if (_library.Rotors.Any(r => r.Name.Equals(imeRotora, StringComparison.OrdinalIgnoreCase)))
            {
                if (MessageBox.Show("Rotor sa tim imenom već postoji. Zameniti?", "Potvrda",
                    MessageBoxButtons.YesNo) == DialogResult.No) 
                    return;

                _library.Rotors.RemoveAll(r => r.Name.Equals(imeRotora, StringComparison.OrdinalIgnoreCase));
            }

            _library.Rotors.Add(new StandardRotor
            {
                Name = imeRotora,
                Wiring = this.Wiring,
                Notch = this.Notch
            });

            EnigmaLibraryManager.Instance.Save(_library);
            PopuniBiblioteku();
            Logger.Logger.Instance.Log("Uspesno je dodat nevi rotor u biblioteku.", Logger.LogType.Info);
            MessageBox.Show("Rotor je uspešno validiran i sačuvan!");
        }

        private bool ValidnaPermutacija(int[] wiring)
        {
            if (wiring == null || wiring.Length == 0) 
                return false;
            var uniqueElements = new HashSet<int>(wiring);
            return uniqueElements.Count == wiring.Length &&
                   wiring.All(x => x >= 0 && x < wiring.Length);
        }

        private string ProveriSveParametre(out string porukaGreske)
        {
            porukaGreske = "";
            int[] w = this.Wiring;
            int n = w.Length;

            if (!ValidnaPermutacija(w))
            {
                porukaGreske = "Ožičenje (Wiring) mora biti validna permutacija (svako slovo se pojavljuje tačno jednom).";
                return porukaGreske;
            }

            if (Notch < 0 || Notch >= n)
            {
                porukaGreske = $"Notch mora biti u opsegu od 0 do {n - 1}.";
                return porukaGreske;
            }

            return null;
        }

        private void ValidirajDuzinu()
        {
            int[] w = this.Wiring;
            bool duzinaOk = w.Length == _blockSize;
            bool permutacijaOk = ValidnaPermutacija(w);

            if (!duzinaOk)
                txtWiring.BackColor = Color.MistyRose;
            else if (!permutacijaOk)
                txtWiring.BackColor = Color.Orange; 
            else
                txtWiring.BackColor = Color.White;

            /*if (w.Length > 0)
            {
                numNotch.Maximum = w.Length - 1;
                numRing.Maximum = w.Length - 1;
                numKey.Maximum = w.Length - 1;
            }*/
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        public void UpdateBlockSize(int newSize)
        {
            _blockSize = newSize;
            ValidirajDuzinu();
        }
    }
}
