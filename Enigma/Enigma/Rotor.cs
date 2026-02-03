using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    internal class Rotor
    {
        private int[] permutacija;
        private int[] inverznaPermutacija;
        public int Pozicija { get; private set; }
        private int N;
        private int notch;

        public Rotor(int[] permutacija, int notch)
        {
            this.permutacija = permutacija;
            N = permutacija.Length;
            Pozicija = 0;
            this.notch = notch;
            inverznaPermutacija = new int[N];
            for (int i = 0; i < N; i++)
                inverznaPermutacija[permutacija[i]] = i;
        }

        public int Napred(int x)
        {
            return permutacija[(x + Pozicija) % N];
        }

        public int Nazad(int y)
        {
            for (int i = 0; i < N; i++)
                if (permutacija[(i + Pozicija) % N] == y)
                    return i;
            throw new Exception("Pogresan unos");
        }

        public void Korak() => Pozicija = (Pozicija + 1) % N;

        public void PostaviPocetnuPoziciju(int pocetnaPozicija)
        {
            if (pocetnaPozicija < 0 || pocetnaPozicija >= N) 
                throw new Exception("Pogresna pocetna pozicijia");
            this.Pozicija = pocetnaPozicija;            
        }

        public bool NaNotchu() => Pozicija == notch;

        public void PomeriNotch(int ringPomeraj)
        {
            notch = (notch - ringPomeraj + N) % N;
        }

        public int ElementNaPoziciji(int i) => permutacija[i];

        public int ElementNaInverznojPoziciji(int i) => inverznaPermutacija[i];
    }
}
