using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    internal class StanjeEnigme
    {
        private Rotor[] nizRotora;
        private int[] ringSettings;
        private int[] plugBoard;
        private int[] reflektor;

        public StanjeEnigme(Rotor[] nizRotora, int[] ringSettings, int[] plugBoard, int[] reflektor)
        {
            this.nizRotora = nizRotora;
            this.ringSettings = ringSettings;
            this.plugBoard = plugBoard;
            this.reflektor = reflektor;
        }

        private void PomeriPozicijRotora()
        {
            bool[] koraci;
            int n = nizRotora.Length;
            koraci = new bool[n];
            koraci[n - 1] = true;
            for (int i = n - 2; i >= 0; i--)
                koraci[i] = nizRotora[i].NaNotchu() || nizRotora[i + 1].NaNotchu();

            koraci[0] = nizRotora[1].NaNotchu();

            for (int i = 0; i < n; i++)
                if (koraci[i])
                    nizRotora[i].Korak();
        }

        private int VratiIzlazIzRotora(int i, int ulaz, bool inverzna)
        {
            int osnova = reflektor.Length;
            int offset = (nizRotora[i].Pozicija - ringSettings[i] + osnova) % osnova;
            int input = (ulaz + offset) % osnova;
            int output = inverzna ? nizRotora[i].ElementNaInverznojPoziciji(input) : nizRotora[i].ElementNaPoziciji(input);
            return (output - offset + osnova) % osnova;
        }

        public int VratiSledeceSlovo(int ulaz)
        {
            PomeriPozicijRotora();
            int n = nizRotora.Length;

            int trenutniPodatak = plugBoard[ulaz];
            for (int i = n - 1; i >= 0; i--)
                trenutniPodatak = VratiIzlazIzRotora(i, trenutniPodatak, false);
            trenutniPodatak = reflektor[trenutniPodatak];
            for (int i = 0; i < n; i++)
                trenutniPodatak = VratiIzlazIzRotora(i, trenutniPodatak, true);
            int izlaz = plugBoard[trenutniPodatak];

            return izlaz;

        }
    }
}
