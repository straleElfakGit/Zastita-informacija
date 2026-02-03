using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enigma
{
    internal class Program
    {
        static void PisiNiz(int[] niz)
        {
            for (int i = 0; i < niz.Length; i++)
                Console.Write(niz[i] + " ");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            int[] permutacija = Generator.Permutacija26();
            PisiNiz(permutacija);
        }
    }
}
