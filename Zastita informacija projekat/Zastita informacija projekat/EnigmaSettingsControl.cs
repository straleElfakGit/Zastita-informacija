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
            PopuniBibliotekuReflektora();

            numTotalRotors.ValueChanged += numTotalRotors_ValueChanged;
            numCurrentIndex.ValueChanged += numCurrentIndex_ValueChanged;
            txtReflector.TextChanged += (s, e) => _desilaSePromena = true;
            txtPlugboard.TextChanged += (s, e) => _desilaSePromena = true;
        }

        private void EnigmaSettingsControl_Load(object sender, EventArgs e)
        {
        }

        private void PopuniBibliotekuReflektora()
        {
            cbReflectorLibrary.Items.Clear();
            cbReflectorLibrary.Items.Add("Custom / Ručni unos");
            foreach (var r in _enigmaLibrary.Reflectors)
            {
                cbReflectorLibrary.Items.Add(r);
            }
            cbReflectorLibrary.DisplayMember = "Name";
            cbReflectorLibrary.SelectedIndex = 0;
            cbReflectorLibrary.DropDownStyle = ComboBoxStyle.DropDown;
        }

        private void UcitajKontrole()
        {
            int count = _enigmaSettings.RotorCount > 0 ? _enigmaSettings.RotorCount : 3;
            numTotalRotors.Value = count;
            numBlockSize.Value = _enigmaSettings.BlockSize;

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
                var novaKontrola = new SingleRotorControl(_enigmaLibrary, index, (int)numBlockSize.Value);
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
                    EnigmaSettingsManager.Instance.Save(_enigmaSettings);
                    Logger.Logger.Instance.Log("Uspešno su sačuvana podešavanja za algoritam Enigma.", LogType.Info);
                    MessageBox.Show("Enigma uspešno sačuvana!");
                }
                else
                {
                    MessageBox.Show("Podešavanja nisu validna! Proverite da li svi rotori imaju ispravnu dužinu ožičenja (" + _enigmaSettings.BlockSize + ").");
                    Logger.Logger.Instance.Log("Podešavanja za enigmu nisu sačuvana: Nekonzistentna podešavanja.", LogType.Error);
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
            ValidirajReflektor();
            _desilaSePromena = true;

            if (cbReflectorLibrary.SelectedIndex != -1)
            {
                cbReflectorLibrary.SelectedIndexChanged -= cbReflectorLibrary_SelectedIndexChanged;
                cbReflectorLibrary.SelectedIndex = -1;
                cbReflectorLibrary.Text = "Novi Reflektor";
                cbReflectorLibrary.SelectedIndexChanged += cbReflectorLibrary_SelectedIndexChanged;
            }
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

        private void cbReflectorLibrary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbReflectorLibrary.SelectedItem is StandardReflector r)
            {
                txtReflector.Text = EnigmaUtils.IntArrayToString(r.Wiring);
                _desilaSePromena = true;
            }
        }

        private bool ValidnaInvolucija(int[] wiring, out string greska)
        {
            greska = "";
            if (wiring == null || wiring.Length == 0) 
                return false;

            for (int i = 0; i < wiring.Length; i++)
            {
                if (wiring[i] == i)
                {
                    greska = $"Slovo na poziciji {i} se preslikava u sebe, što nije dozvoljeno za reflektor.";
                    return false;
                }
                int partner = wiring[i];
                if (partner < 0 || partner >= wiring.Length || wiring[partner] != i)
                {
                    greska = $"Nevalidan par: {i} ide u {partner}, ali {partner} ne ide nazad u {i}.";
                    return false;
                }
            }
            return true;
        }

        private void ValidirajReflektor()
        {
            int[] w = EnigmaUtils.StringToIntArray(txtReflector.Text);
            bool duzinaOk = w.Length == (int)numBlockSize.Value;
            string greska;
            bool involucijaOk = ValidnaInvolucija(w, out greska);

            if (!duzinaOk) 
                txtReflector.BackColor = Color.MistyRose;
            else if (!involucijaOk) 
                txtReflector.BackColor = Color.Orange;
            else 
                txtReflector.BackColor = Color.White;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int[] w = EnigmaUtils.StringToIntArray(txtReflector.Text);
            string greska;

            if (!ValidnaInvolucija(w, out greska))
            {
                MessageBox.Show(greska, "Nevalidan reflektor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Logger.Logger.Instance.Log("Nije uspelo cuvanje reflektora u biblioteku: Nevalidan unos za reflektor.", LogType.Error);
                return;
            }

            string ime = cbReflectorLibrary.Text;
            if (string.IsNullOrWhiteSpace(ime) || ime == "Custom / Ručni unos")
            {
                MessageBox.Show("Unesite naziv reflektora.");
                return;
            }
            _enigmaLibrary.Reflectors.RemoveAll(r => r.Name.Equals(ime, StringComparison.OrdinalIgnoreCase));

            _enigmaLibrary.Reflectors.Add(new StandardReflector
            {
                Name = ime,
                Wiring = w
            });

            EnigmaLibraryManager.Instance.Save(_enigmaLibrary);
            PopuniBibliotekuReflektora();
            Logger.Logger.Instance.Log("Uspešno sačuvan reflektor u biblioteku.", LogType.Info);
            MessageBox.Show("Reflektor sačuvan!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var rezultat = MessageBox.Show("Da li ste sigurni da želite da poništite sve izmene?",
                                 "Potvrda reseta", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (rezultat == DialogResult.Yes)
            {
                UcitajKontrole();
                Logger.Logger.Instance.Log("Podešavanja Enigme su resetovana na poslednje sačuvane vrednosti.", LogType.Info);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _enigmaSettings.BlockSize = (int)numBlockSize.Value;
            SaveSettings();
        }

        private void numBlockSize_ValueChanged(object sender, EventArgs e)
        {
            int novaVelicina = (int)numBlockSize.Value;
            //_enigmaSettings.BlockSize = novaVelicina;

            foreach (var rotorCtrl in _rotorsInMemory)
            {
                rotorCtrl.UpdateBlockSize(novaVelicina);
            }

            ValidirajReflektor();
            _desilaSePromena = true;
        }
    }
}
