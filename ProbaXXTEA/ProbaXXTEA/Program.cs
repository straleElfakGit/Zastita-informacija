using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XXTEA;
using TokoviPodataka;

namespace ProbaXXTEA
{
    internal class Program
    {
        static void PisiNiz(byte[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
                Console.Write(niz[i] + " ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            var key = new uint[]
            {
                0x01234567, 0x89ABCDEF, 0xFEDCBA98, 0x76543210
            };

            key = XXTEAGeneratorKljuca.GenerateKey();
            var sifrator = new XXTEASifrator(key, 2); // 64-bit blok

            /*byte[] data = {
                1,2,3,4, 5,6,7,8 ,9,10,11,12, 13,14,15,16
            };

            PisiNiz(data);
            var enc = sifrator.Encrypt(data);
            PisiNiz(enc);
            var dec = sifrator.Decrypt(enc);
            PisiNiz(dec);*/

            string ulazniFilm = "Aladdin - Return of Jafar.avi";
            string sifrovaniFilm = "Aladdin_sifrovano.dat";
            string dekriptovaniFilm = "Aladdin_dekriptovano.avi";

            if (!File.Exists(ulazniFilm))
            {
                Console.WriteLine("Film nije pronađen!");
                return;
            }

            Console.WriteLine("Započinjem enkripciju filma...");
            DateTime startEnc = DateTime.Now;

            using (FileStream fsUlaz = new FileStream(ulazniFilm, FileMode.Open, FileAccess.Read))
            using (FileStream fsIzlaz = new FileStream(sifrovaniFilm, FileMode.Create, FileAccess.Write))
            using (SifratorByteStream sbs = new SifratorByteStream(fsIzlaz, sifrator, readMode: false))
            {
                byte[] buffer = new byte[64 * 1024];
                int bytesRead;

                while ((bytesRead = fsUlaz.Read(buffer, 0, buffer.Length)) > 0)
                {
                    int ostatak = bytesRead % sifrator.VelicinaBloka;
                    if (ostatak != 0)
                    {
                        int padding = sifrator.VelicinaBloka - ostatak;
                        Array.Clear(buffer, bytesRead, padding);
                        sbs.Write(buffer, 0, bytesRead + padding);
                    }
                    else
                    {
                        sbs.Write(buffer, 0, bytesRead);
                    }
                }
            }
            Console.WriteLine($"Enkripcija gotova za: {DateTime.Now - startEnc}");

            Console.WriteLine("Započinjem dekripciju filma...");
            DateTime startDec = DateTime.Now;

            using (FileStream fsSifrovano = new FileStream(sifrovaniFilm, FileMode.Open, FileAccess.Read))
            using (FileStream fsKonacno = new FileStream(dekriptovaniFilm, FileMode.Create, FileAccess.Write))
            using (SifratorByteStream sbs = new SifratorByteStream(fsSifrovano, sifrator, readMode: true))
            {
                sbs.CopyTo(fsKonacno);
            }
            Console.WriteLine($"Dekripcija gotova za: {DateTime.Now - startDec}");

            Console.WriteLine("\nTest završen. Probaj da pustiš 'Aladdin_dekriptovano.avi'!");
        }
    }
}
