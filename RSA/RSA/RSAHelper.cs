using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace RSA
{
    public static class RSAHelper
    {
        public static ulong Compute(ulong data, uint exponent, ulong n)
        {
            if (n == 0) throw new ArgumentException("N ne sme biti nula.");
            if (n == 1)
                return 0;

            BigInteger res = 1;
            BigInteger baseVal = data % n;
            BigInteger modN = n;

            uint exp = exponent;
            while (exp > 0)
            {
                if ((exp & 1) == 1)
                    res = (res * baseVal) % modN;

                baseVal = (baseVal * baseVal) % modN;
                exp >>= 1;
            }

            return (ulong)res;
        }
    }
}
