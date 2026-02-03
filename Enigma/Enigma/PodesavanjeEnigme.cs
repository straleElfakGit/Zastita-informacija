using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    internal class PodesavanjeEnigme
    {
        private int[][] permutacijeRotora;
        private int[] notcheviRotora;
        private int[] keySettings;
        private int[] ringSettings;
        private int[] reflektor;
        private int[] plugBoard;

        public PodesavanjeEnigme(int[][] permutacijeRotora, int[] notcheviRotora, int[] keySettings, int[] ringSettings, int[] reflektor, int[] plugBoard)
        {
            this.permutacijeRotora = permutacijeRotora;
            this.notcheviRotora = notcheviRotora;
            this.keySettings = keySettings;
            this.ringSettings = ringSettings;
            this.reflektor = reflektor;
            this.plugBoard = plugBoard;
        }

        public StanjeEnigme vratiPocetnoStanjeEnigme()
        {
            int n = notcheviRotora.Length;
            Rotor[] nizRotora;
            nizRotora = new Rotor[n];
            for (int i = 0; i < n; i++)
            {
                nizRotora[i] = new Rotor(permutacijeRotora[i], notcheviRotora[i]);
                nizRotora[i].PostaviPocetnuPoziciju(keySettings[i]);
                //nizRotora[i].PomeriNotch(ringSettings[i]);
            }
            return new StanjeEnigme(nizRotora, ringSettings, plugBoard, reflektor);
        }
    }
}
