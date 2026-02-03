using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    public static class Generator
    {
        #region javne metode

        #region duge metode
        public static int[] Permutacija(ulong seed, int n)
        {
            int[] niz = Enumerable.Range(0, n).ToArray();
            ulong x = seed;

            for (int i = n - 1; i > 0; i--)
            {
                Izmesaj(ref x);
                int j = (int)(x % (ulong)(i + 1));
                Swap(ref niz[i], ref niz[j]);
            }

            return niz;
        }

        public static int[] GenerisiPlugboard(ulong seed, int n)
        {
            int[] plug = new int[n];
            for (int i = 0; i < n; i++)
                plug[i] = -1;

            List<int> slobodni = Enumerable.Range(0, n).ToList();
            ulong x = seed;

            while (slobodni.Count > 0)
            {
                int i = slobodni[slobodni.Count - 1];
                slobodni.RemoveAt(slobodni.Count - 1);

                if (plug[i] != -1)
                    continue;

                Izmesaj(ref x);

                if (slobodni.Count == 0 || (x & 1) == 0)
                    plug[i] = i;                
                else
                {
                    int idx = (int)(x % (ulong)slobodni.Count);
                    int j = slobodni[idx];
                    slobodni.RemoveAt(idx);

                    plug[i] = j;
                    plug[j] = i;
                }
            }

            return plug;
        }

        public static int[] GenerisiPlugboardSaParovima(ulong seed, int brojParova, int n)
        {
            if (brojParova < 0 || brojParova > n / 2)
                throw new ArgumentException("Max " + n/ 2 + " parova");

            int[] plug = new int[n];
            for (int i = 0; i < n; i++)
                plug[i] = i;

            List<int> slobodni = Enumerable.Range(0, n).ToList();
            ulong x = seed;

            for (int p = 0; p < brojParova; p++)
            {
                Izmesaj(ref x);

                int i = slobodni[(int)(x % (ulong)slobodni.Count)];
                slobodni.Remove(i);

                Izmesaj(ref x);

                int j = slobodni[(int)(x % (ulong)slobodni.Count)];
                slobodni.Remove(j);

                plug[i] = j;
                plug[j] = i;
            }

            return plug;
        }

        #endregion

        #region krace metode
        public static int[] Permutacija26(ulong seed)
        {
            return Permutacija(seed, 26);
        }

        public static int[] Permutacija26()
        {
            return Permutacija((ulong)DateTime.Now.Ticks, 26);
        }

        public static int[] Permutacija(int n)
        {
            return Permutacija((ulong)DateTime.Now.Ticks, n);
        }

        public static int[] GenerisiPlugboard(int n)
        {
            return GenerisiPlugboard((ulong)DateTime.Now.Ticks, n);
        }

        public static int[] GenerisiPlugboardSaParovima(int brojParova, int n)
        {
            return GenerisiPlugboardSaParovima((ulong)DateTime.Now.Ticks, brojParova, n);
        }

        public static int[] GenerisiPlugboard26()
        {
            return GenerisiPlugboard((ulong)DateTime.Now.Ticks, 26);
        }

        public static int[] GenerisiPlugboardSaParovima26(int brojParova)
        {
            return GenerisiPlugboardSaParovima((ulong)DateTime.Now.Ticks, brojParova, 26);
        }

        #endregion

        #region standardne permutacije

        public static int[] RotorI()
        {
            return new int[]
            {
                4, 10, 12, 5, 11, 6, 3, 16, 21, 25,
                13, 19, 14, 22, 24, 7, 23, 20, 18, 15,
                0, 8, 1, 17, 2, 9
            };
        }

        public static int[] RotorII()
        {
            return new int[]
            {
                0, 9, 3, 10, 18, 8, 17, 20, 23, 1,
                11, 7, 22, 19, 12, 2, 16, 6, 25, 13,
                15, 24, 5, 21, 14, 4
            };
        }

        public static int[] RotorIII()
        {
            return new int[]
            {
                1, 3, 5, 7, 9, 11, 2, 15, 17, 19,
                23, 21, 25, 13, 24, 4, 8, 22, 6, 0,
                10, 12, 20, 18, 16, 14
            };
        }

        public static int[] RotorIV()
        {
            return new int[]
            {
                4, 18, 14, 21, 15, 25, 9, 0, 24, 16,
                20, 8, 17, 7, 23, 11, 13, 5, 19, 6,
                10, 3, 2, 12, 22, 1
            };
        }

        public static int[] RotorV()
        {
            return new int[]
            {
                21, 25, 1, 17, 6, 8, 19, 24, 20, 15,
                18, 3, 13, 7, 11, 23, 0, 22, 12, 9,
                16, 14, 5, 4, 2, 10
            };
        }

        public static int[] RotorVI()
        {
            return new int[]
            {
                9, 15, 6, 21, 14, 20, 12, 5, 24, 16,
                1, 4, 13, 7, 25, 17, 3, 10, 0, 18,
                23, 11, 8, 2, 19, 22
            };
        }

        public static int[] RotorVII()
        {
            return new int[]
            {
                13, 25, 9, 7, 6, 17, 2, 23, 12, 24,
                18, 22, 1, 14, 20, 5, 0, 8, 21, 11,
                15, 4, 10, 19, 3, 16
            };
        }

        public static int[] RotorVIII()
        {
            return new int[]
            {
                5, 10, 16, 7, 19, 11, 23, 14, 2, 1,
                9, 18, 15, 3, 25, 17, 0, 12, 4, 22,
                13, 8, 20, 24, 6, 21
            };
        }

        public static int[] RotorBeta()
        {
            return new int[]
            {
                11, 4, 24, 9, 21, 2, 13, 8, 23, 22,
                15, 1, 16, 12, 3, 17, 19, 0, 10, 25,
                6, 5, 20, 7, 14, 18
            };
        }

        public static int[] RotorGamma()
        {
            return new int[]
            {
                5, 18, 14, 10, 0, 13, 20, 4, 17, 7,
                12, 1, 19, 8, 24, 2, 22, 11, 16, 15,
                25, 23, 21, 6, 9, 3
            };
        }

        #endregion

        #region standardni reflektori

        public static int[] ReflektorA()
        {
            return new int[]
            {
                4, 9, 12, 25, 0, 11, 24, 23, 21, 1,
                22, 5, 2, 17, 16, 20, 14, 13, 19, 18,
                15, 8, 10, 7, 6, 3
            };
        }

        public static int[] ReflektorB()
        {
            return new int[]
            {
                24, 17, 20, 7, 16, 18, 11, 3, 15, 23,
                13, 6, 14, 10, 12, 8, 4, 1, 5, 25,
                2, 22, 21, 9, 0, 19
            };
        }

        public static int[] ReflektorC()
        {
            return new int[]
            {
                5, 21, 15, 9, 8, 0, 14, 24, 4, 3,
                17, 25, 23, 22, 6, 2, 19, 10, 20, 16,
                18, 1, 13, 12, 7, 11
            };
        }

        public static int[] ReflektorBThin()
        {
            return new int[]
            {
                4, 13, 10, 16, 0, 20, 24, 22, 9, 8,
                2, 14, 15, 1, 11, 12, 3, 23, 25, 21,
                5, 19, 7, 17, 6, 18
            };
        }

        public static int[] ReflektorCThin()
        {
            return new int[]
            {
                17, 3, 14, 1, 9, 13, 19, 10, 21, 4,
                7, 12, 11, 5, 2, 22, 25, 0, 23, 6,
                24, 8, 15, 18, 20, 16
            };
        }

        #endregion

        #region Notch pozicije za rotore

        public static Dictionary<string, int> NotcheviRotora()
        {
            return new Dictionary<string, int>
            {
                { "I", 16 },    // Q
                { "II", 4 },    // E
                { "III", 21 },  // V
                { "IV", 9 },    // J
                { "V", 25 },    // Z
                
                { "VI", 12 },   // M (i Z) - ali kodirajmo samo prvi
                { "VII", 12 },  // M (i Z)
                { "VIII", 12 }, // M (i Z)
                { "Beta", -1 }, // Nema notch
                { "Gamma", -1 } // Nema notch
            };
        }

        public static int NotchZaRotor(string imeRotora)
        {
            var notchevi = NotcheviRotora();
            if (notchevi.ContainsKey(imeRotora))
                return notchevi[imeRotora];
            return -1;
        }


        #endregion

        #region Metode za konverziju

        public static int[] StringUPermutaciju(string wiring, bool isReflector = false)
        {
            int[] permutacija = new int[wiring.Length];
            for (int i = 0; i < wiring.Length; i++)
            {
                permutacija[i] = wiring[i] - 'A';
            }
            return permutacija;
        }

        public static string PermutacijaUString(int[] permutacija)
        {
            char[] chars = new char[permutacija.Length];
            for (int i = 0; i < permutacija.Length; i++)
            {
                chars[i] = (char)('A' + permutacija[i]);
            }
            return new string(chars);
        }

        #endregion

        #endregion

        #region privatne metode
        private static void Swap(ref int x, ref int y)
        {
            int temp = x;
            x = y;
            y = temp;
        }

        private static void Izmesaj(ref ulong x)
        {
            x ^= x << 13;
            x ^= x >> 7;
            x ^= x << 17;
        }

        #endregion
    }
}
