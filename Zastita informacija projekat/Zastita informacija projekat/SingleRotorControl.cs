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
            if (cbLibrary.SelectedIndex != 0 && sender == txtWiring) 
                cbLibrary.SelectedIndex = 0;
            PodaciIzmenjeni?.Invoke(this, EventArgs.Empty);
        }

        private void SingleRotorControl_Load(object sender, EventArgs e)
        {

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

        }
        private void ValidirajDuzinu()
        {
            bool isOk = txtWiring.Text.Length == _blockSize;
            txtWiring.BackColor = isOk ? Color.White : Color.MistyRose;
        }
    }
}
