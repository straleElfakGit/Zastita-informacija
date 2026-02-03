using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal class TigerBlockProcesor : ITigerBlockProcesor
    {
        private readonly ISBox _sbox;

        public TigerBlockProcesor(ISBox sbox)
        {
            _sbox = sbox;
        }

        void ITigerBlockProcesor.ProcessBlock(byte[] block512, TigerKontekst context)
        {
            InterlanProcessBlock(block512, context);
        }

        public void ProcessBlock(byte[] block512, TigerKontekst context)
        {
            InterlanProcessBlock(block512, context);
        }

        private void InterlanProcessBlock(byte[] block, TigerKontekst ctx)
        {
            ulong[] w = new ulong[8];

            for (int i = 0; i < 8; i++)
                w[i] = ToUInt64LE(block, i * 8);

            ulong a = ctx.H0;
            ulong b = ctx.H1;
            ulong c = ctx.H2;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    c ^= w[j];

                    byte c0 = (byte)(c);
                    byte c1 = (byte)(c >> 8);
                    byte c2 = (byte)(c >> 16);
                    byte c3 = (byte)(c >> 24);
                    byte c4 = (byte)(c >> 32);
                    byte c5 = (byte)(c >> 40);
                    byte c6 = (byte)(c >> 48);
                    byte c7 = (byte)(c >> 56);

                    a -= _sbox.S0[c0] ^ _sbox.S1[c2] ^ _sbox.S2[c4] ^ _sbox.S3[c6];
                    b += _sbox.S3[c1] ^ _sbox.S2[c3] ^ _sbox.S1[c5] ^ _sbox.S0[c7];
                    b *= (ulong)(i + 1);
                }

                KeySchedule(w, i);
            }

            ctx.Add(a, b, c);
        }

        private static void KeySchedule(ulong[] w, int i)
        {
            if (i == 0)
            {
                w[0] -= w[7] ^ 0xA5A5A5A5A5A5A5A5;
                w[1] ^= w[0];
                w[2] += w[1];
                w[3] -= w[2] ^ ((w[1] ^ ulong.MaxValue) << 19);
                w[4] ^= w[3];
                w[5] += w[4];
                w[6] -= w[5] ^ ((w[4] ^ ulong.MaxValue) >> 23);
                w[7] ^= w[6];
            }
            else if (i == 1)
            {
                w[0] += w[7];
                w[1] -= w[0] ^ ((w[0] ^ ulong.MaxValue) << 19);
                w[2] ^= w[1];
                w[3] += w[2];
                w[4] -= w[3] ^ ((w[2] ^ ulong.MaxValue) >> 23);
                w[5] ^= w[4];
                w[6] += w[5];
                w[7] -= w[6] ^ 0x0123456789ABCDEF;
            }
        }

        private static ulong ToUInt64LE(byte[] buffer, int offset)
        {
            return
                ((ulong)buffer[offset + 0]) |
                ((ulong)buffer[offset + 1] << 8) |
                ((ulong)buffer[offset + 2] << 16) |
                ((ulong)buffer[offset + 3] << 24) |
                ((ulong)buffer[offset + 4] << 32) |
                ((ulong)buffer[offset + 5] << 40) |
                ((ulong)buffer[offset + 6] << 48) |
                ((ulong)buffer[offset + 7] << 56);
        }
    }
}
