using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using XXTEA;
using CFBMod;

namespace CFBProba
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
            uint[] key;
            key = XXTEAGeneratorKljuca.GenerateKey();
            var sifrator = new XXTEASifrator(key, 2);

            byte[] data = {
                1,2,3,4, 5,6,7,8 ,9,10,11,12, 13,14,15,16
            };

            CFBModSifrator modSifrator = new CFBModSifrator(sifrator, 32);
            byte[] iv = IVGenerator.GenerisiIV(8);
            modSifrator.Init(data, iv);

            PisiNiz(data);
            var enc = modSifrator.Encrypt(data);
            PisiNiz(enc);
            modSifrator.ResetState();
            var dec = modSifrator.Decrypt(enc);
            PisiNiz(dec);
        }
    }
}
