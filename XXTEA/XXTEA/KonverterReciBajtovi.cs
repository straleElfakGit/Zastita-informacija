using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXTEA
{
    internal static class KonverterReciBajtovi
    {
        public static uint[] BytesToWords(byte[] data, int offset, int brojReciPoBloku)
        {
            uint[] v = new uint[brojReciPoBloku];

            for (int i = 0; i < brojReciPoBloku; i++)
            {
                v[i] =
                    (uint)(data[offset + 4 * i] |
                          data[offset + 4 * i + 1] << 8 |
                          data[offset + 4 * i + 2] << 16 |
                          data[offset + 4 * i + 3] << 24);
            }

            return v;
        }

        public static void WordsToBytes(uint[] v, byte[] data, int offset, int brojReciPoBloku)
        {
            for (int i = 0; i < brojReciPoBloku; i++)
            {
                uint w = v[i];
                data[offset + 4 * i] = (byte)(w & 0xFF);
                data[offset + 4 * i + 1] = (byte)((w >> 8) & 0xFF);
                data[offset + 4 * i + 2] = (byte)((w >> 16) & 0xFF);
                data[offset + 4 * i + 3] = (byte)((w >> 24) & 0xFF);
            }
        }
    }
}
