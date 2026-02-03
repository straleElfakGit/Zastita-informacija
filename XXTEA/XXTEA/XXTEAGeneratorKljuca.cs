using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XXTEA
{
    public static class XXTEAGeneratorKljuca
    {
        public static uint[] GenerateKey()
        {
            byte[] bytes = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(bytes);
            }

            return new uint[]
            {
                BitConverter.ToUInt32(bytes, 0),
                BitConverter.ToUInt32(bytes, 4),
                BitConverter.ToUInt32(bytes, 8),
                BitConverter.ToUInt32(bytes, 12)
            };
        }

        private static readonly Random rng = new Random();

        public static uint[] GenerateKey2()
        {
            return new uint[]
            {
                NextUInt32(),
                NextUInt32(),
                NextUInt32(),
                NextUInt32()
            };
        }

        private static uint NextUInt32()
        {
            byte[] b = new byte[4];
            rng.NextBytes(b);
            return BitConverter.ToUInt32(b, 0);
        }
    }
}
