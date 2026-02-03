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
    public partial class EnigmaSettingsControl : UserControl, ISettingsControl
    {
        private EnigmaSettings _enigmaSettings;
        private EnigmaLibrary _enigmaLibrary;
        private bool _desilaSePromena;
        private List<SingleRotorControl> _rotorsInMemory = new List<SingleRotorControl>();
        public bool DesilaSePromena { get => _desilaSePromena; private set => _desilaSePromena = value; }
        public EnigmaSettingsControl(EnigmaSettings enigmaSettings, EnigmaLibrary enigmaLibrary)
        {
            InitializeComponent();
            _enigmaLibrary = enigmaLibrary;
            _enigmaSettings = enigmaSettings;

            numTotalRotors.Minimum = 3;
            numTotalRotors.Maximum = 8;

            UcitajKontrole();

            numTotalRotors.ValueChanged += numTotalRotors_ValueChanged;
            numCurrentIndex.ValueChanged += numCurrentIndex_ValueChanged;
            txtReflector.TextChanged += (s, e) => _desilaSePromena = true;
            txtPlugboard.TextChanged += (s, e) => _desilaSePromena = true;
        }

        private void EnigmaSettingsControl_Load(object sender, EventArgs e)
        {
        }
        private void UcitajKontrole()
        {
            int count = _enigmaSettings.RotorCount > 0 ? _enigmaSettings.RotorCount : 3;
            numTotalRotors.Value = count;

            AzurirajListuRotora(count);

            for (int i = 0; i < _rotorsInMemory.Count; i++)
            {
                if (i < _enigmaSettings.PermutacijeRotora.Length)
                {
                    _rotorsInMemory[i].UcitajPodatke(
                        _enigmaSettings.PermutacijeRotora[i],
                        _enigmaSettings.NotcheviRotora[i],
                        _enigmaSettings.RingSettings[i],
                        _enigmaSettings.KeySettings[i]
                    );
                }
            }

            txtReflector.Text = EnigmaUtils.IntArrayToString(_enigmaSettings.Reflektor);
            txtPlugboard.Text = EnigmaUtils.IntArrayToString(_enigmaSettings.PlugBoard);

            numCurrentIndex.Value = 1;
            PrikaziRotor(0);

            _desilaSePromena = false;
        }

        private void AzurirajListuRotora(int noviBroj)
        {
            while (_rotorsInMemory.Count < noviBroj)
            {
                int index = _rotorsInMemory.Count;
                var novaKontrola = new SingleRotorControl(_enigmaLibrary, index, _enigmaSettings.BlockSize);
                novaKontrola.PodaciIzmenjeni += (s, e) => _desilaSePromena = true;
                _rotorsInMemory.Add(novaKontrola);
            }

            while (_rotorsInMemory.Count > noviBroj)
            {
                var visak = _rotorsInMemory[_rotorsInMemory.Count - 1];
                visak.Dispose();
                _rotorsInMemory.RemoveAt(_rotorsInMemory.Count - 1);
            }
            numCurrentIndex.Maximum = noviBroj;
        }

        private void PrikaziRotor(int index)
        {
            if (index < 0 || index >= _rotorsInMemory.Count) return;

            panelActiveRotor.Controls.Clear();

            var kontrolaZaPrikaz = _rotorsInMemory[index];
            kontrolaZaPrikaz.Dock = DockStyle.Fill;
            panelActiveRotor.Controls.Add(kontrolaZaPrikaz);
        }

        public void SaveSettings()
        {
            try
            {
                int count = _rotorsInMemory.Count;

                var novePermutacije = new int[count][];
                var noviNotchevi = new int[count];
                var noviRing = new int[count];
                var noviKey = new int[count];

                for (int i = 0; i < count; i++)
                {
                    var ctrl = _rotorsInMemory[i];
                    novePermutacije[i] = ctrl.Wiring;
                    noviNotchevi[i] = ctrl.Notch;
                    noviRing[i] = ctrl.RingSetting;
                    noviKey[i] = ctrl.KeySetting;
                }

                _enigmaSettings.PermutacijeRotora = novePermutacije;
                _enigmaSettings.NotcheviRotora = noviNotchevi;
                _enigmaSettings.RingSettings = noviRing;
                _enigmaSettings.KeySettings = noviKey;
                _enigmaSettings.Reflektor = EnigmaUtils.StringToIntArray(txtReflector.Text);
                _enigmaSettings.PlugBoard = EnigmaUtils.StringToIntArray(txtPlugboard.Text);

                if (_enigmaSettings.ConsistantSettings())
                {
                    _desilaSePromena = false;
                    MessageBox.Show("Enigma uspešno sačuvana!");
                }
                else
                {
                    MessageBox.Show("Podešavanja nisu validna! Proverite da li svi rotori imaju ispravnu dužinu ožičenja (" + _enigmaSettings.BlockSize + ").");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Greška: " + ex.Message);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtReflector_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtPlugboard_TextChanged(object sender, EventArgs e)
        {

        }

        private void numTotalRotors_ValueChanged(object sender, EventArgs e)
        {
            AzurirajListuRotora((int)numTotalRotors.Value);
            _desilaSePromena = true;

            if (numCurrentIndex.Value > numTotalRotors.Value)
                numCurrentIndex.Value = numTotalRotors.Value;
        }

        private void numCurrentIndex_ValueChanged(object sender, EventArgs e)
        {
            int index = (int)numCurrentIndex.Value - 1;
            PrikaziRotor(index);
        }
    }
}
