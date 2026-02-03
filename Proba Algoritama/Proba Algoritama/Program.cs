using Enigma;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using TokoviPodataka;

namespace Proba_Algoritama
{
    internal class Program
    {
        static void PisiNiz(int[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
                Console.Write("({0}, {1})", i, niz[i]);
            Console.WriteLine();
        }

        static void PisiNiz2(int[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
                Console.Write((char)(niz[i] + 'A'));
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] permutacija = Generator.Permutacija(30);
            PisiNiz(permutacija);

            int[] plugBoar = Generator.GenerisiPlugboardSaParovima(2, 30);
            PisiNiz(plugBoar);

            /*int[] rotor1 = Generator.RotorI();
            int[] rotor2 = Generator.RotorII();
            int[] rotor3 = Generator.RotorIII();
            PisiNiz2(rotor1);
            PisiNiz2(rotor2);
            PisiNiz2(rotor3);*/

            /* for (int i = 0; i < 128; i++)
             {
                 Console.WriteLine("{0}, {1} {2}", i, (char)i, new EnigmaSymbol(i));
             }*/

            /*for (int i = 0; i < 128; i++)
            {
                Console.WriteLine("{0}, {1} {2}", i, (char)i, new GeneralniEnigmaSymbol(i, 26, 'A'));
            }*/

            int[] pretmutacija1 = Generator.RotorI();
            int[] pretmutacija2 = Generator.RotorII();
            int[] pretmutacija3 = Generator.RotorIII();
            int[][] rotori =
            {
                pretmutacija1,
                pretmutacija2,
                pretmutacija3
            };
            int[] notchevi = { 16, 4, 21 };
            int[] keySettings = { 18, 15, 16 };
            int[] ringSettings = { 4, 0, 1 };
            int[] reflektor = Generator.ReflektorB();
            int[] plugBoard =
            {
                0, 6, 3, 2, 17, 21, 
                1, 13, 20, 10, 9, 12, 
                11, 7, 15, 14, 16, 4, 
                18, 24, 8, 5, 22, 23, 19, 25
            };

            EnigmaSifrator es = new EnigmaSifrator(rotori, notchevi, keySettings, ringSettings, reflektor, plugBoard);
            /*string pocetniTekst = "Dobro, sada ću da uznem neke Srpske reči na latinici ali sa Srpskim slovima.!_Ć!#$#%ćasdf0'34934'0sadfasdfć";
            Console.WriteLine(pocetniTekst);
            es.ResetState();
            string rezultat = es.Encrypt(pocetniTekst);
            Console.WriteLine(rezultat);
            es.ResetState();
            Console.WriteLine(es.Decrypt(rezultat));*/


            string putanjaUlaz = "ASCIIFail100000.txt";
            string putanjaSifrovano = "reci_sifrovano.txt";
            string putanjaDekriptovano = "reci_dekriptovano.txt";

            if (!File.Exists(putanjaUlaz))
                File.WriteAllText(putanjaUlaz, "Ovo je test tekst sa srpskim slovima ŠĐČĆŽ.");

            Console.WriteLine("Započinjem enkripciju...");

            using (FileStream fsUlaz = new FileStream(putanjaUlaz, FileMode.Open, FileAccess.Read))
            using (FileStream fsIzlaz = new FileStream(putanjaSifrovano, FileMode.Create, FileAccess.Write))
            using (SifratorTextStream sts = new SifratorTextStream(fsIzlaz, es, readMode: false, encoding: Encoding.UTF8))
            {
                fsUlaz.CopyTo(sts);
            }
            Console.WriteLine($"Fajl je šifrovan i sačuvan u: {putanjaSifrovano}");

            Console.WriteLine("Započinjem dekripciju...");

            using (FileStream fsSifrovano = new FileStream(putanjaSifrovano, FileMode.Open, FileAccess.Read))
            using (FileStream fsKonacno = new FileStream(putanjaDekriptovano, FileMode.Create, FileAccess.Write))
            using (SifratorTextStream sts = new SifratorTextStream(fsSifrovano, es, readMode: true, encoding: Encoding.UTF8))
            {
                sts.CopyTo(fsKonacno);
            }
            Console.WriteLine($"Fajl je dešifrovan i sačuvan u: {putanjaDekriptovano}");

            Console.WriteLine("\nSadržaj dekriptovanog fajla:");
            //Console.WriteLine(File.ReadAllText(putanjaDekriptovano));

        }
    }
}
