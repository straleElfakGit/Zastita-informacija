using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RSA;

namespace Proba_RSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            uint e, d;
            e = 10859;
            d = 1267111583;
            ulong N = 5577608093;

            byte m = 2;

            ulong rez = RSAHelper.Compute((ulong)m, e, N);
            Console.WriteLine(rez);
            ulong reverse_rez = RSAHelper.Compute(rez, d, N);
            Console.WriteLine((byte)reverse_rez);
        }
    }
}
