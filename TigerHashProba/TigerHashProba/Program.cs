using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TigerHash;
using TokoviPodataka;

namespace TigerHashProba
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*TigerHash.TigerHash hash = new TigerHash.TigerHash(1);
            string tekst = "Ovo je tekst koji sluzi za probu da vidimo sta ce da nam vradi nas tiger hash";
            byte[] bajtovi = Encoding.ASCII.GetBytes(tekst);
            byte[] rez = hash.ComputeHash(bajtovi);
            Console.WriteLine(BitConverter.ToString(rez));*/

            string putanjaDoFilma = "Aladdin - Return of Jafar.avi";

            if (!File.Exists(putanjaDoFilma))
            {
                Console.WriteLine("Film nije pronađen na putanji!");
                return;
            }

            TigerHash.TigerHash tiger = new TigerHash.TigerHash(1);

            Console.WriteLine("Započinjem generisanje Tiger otiska za film...");
            DateTime start = DateTime.Now;

            using (FileStream fs = new FileStream(putanjaDoFilma, FileMode.Open, FileAccess.Read))
            using (HashByteStream hbs = new HashByteStream(fs, tiger))
            {
                hbs.CopyTo(Stream.Null);

                byte[] hashRezultat = hbs.GetResult();

                DateTime end = DateTime.Now;

                Console.WriteLine("\n--- TIGER HASH REZULTAT ---");
                Console.WriteLine(BitConverter.ToString(hashRezultat).Replace("-", ""));
                Console.WriteLine("--------------------------");
                Console.WriteLine($"Vreme obrade: {end - start}");
                Console.WriteLine($"Veličina fajla: {fs.Length / (1024.0 * 1024.0):F2} MB");
            }

            Console.WriteLine("\nIntegritet proveren. Pritisnite bilo koji taster za kraj.");
            Console.ReadKey();
        }
    }
}
