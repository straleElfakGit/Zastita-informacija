using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hijerarhija_Algoritama;

namespace XXTEA
{
    public class XXTEASifrator : BlockSifrator<byte[]>, SifratorBytePodataka
    {
        private const uint DELTA = 0x9E3779B9;
        private readonly uint[] key;
        private readonly int brojReciPoBloku;

        public override int VelicinaBloka => brojReciPoBloku * 4;

        public XXTEASifrator(uint[] key, int brojReciPoBloku)
        {
            if (key == null || key.Length != 4)
                throw new ArgumentException("Kljuc mora da ima 128 bita (4 uints).");

            if (brojReciPoBloku < 2)
                throw new ArgumentException("Blok mora da ima najmanje 64");

            this.key = key;
            this.brojReciPoBloku = brojReciPoBloku;
        }

        #region implementacija nasledjenih metoda

        public override byte[] EncryptBlock(byte[] block)
        {
            if (block == null || block.Length != VelicinaBloka)
                throw new ArgumentException("Neispravna velicina bloka.");

            byte[] output = new byte[VelicinaBloka];
            EncryptBlock(block, 0, output, 0);
            return output;
        }

        byte[] SifratorBytePodataka.Decrypt(byte[] ciphertext)
        {
            return Decrypt(ciphertext);
        }
        public byte[] Decrypt(byte[] ciphertext)
        {
            if (ciphertext == null)
                throw new ArgumentNullException(nameof(ciphertext));

            if (ciphertext.Length % VelicinaBloka != 0)
                throw new ArgumentException("Ulaz nije poravnat na velicinu bloka.");

            byte[] output = new byte[ciphertext.Length];

            for (int offset = 0; offset < ciphertext.Length; offset += VelicinaBloka)
            {
                DecryptBlock(ciphertext, offset, output, offset);
            }

            return output;
        }

        byte[] SifratorBytePodataka.Encrypt(byte[] plaintext)
        {
            return Encrypt(plaintext);
        }

        public byte[] Encrypt(byte[] plaintext)
        {
            if (plaintext == null)
                throw new ArgumentNullException(nameof(plaintext));

            if (plaintext.Length % VelicinaBloka != 0)
                throw new ArgumentException("Ulaz nije poravnat na velicinu bloka.");

            byte[] output = new byte[plaintext.Length];

            for (int offset = 0; offset < plaintext.Length; offset += VelicinaBloka)
            {
                EncryptBlock(plaintext, offset, output, offset);
            }

            return output;
        }

        #endregion

        #region privatne metode

        private void EncryptBlock(byte[] input, int inputOffset,
                             byte[] output, int outputOffset)
        {
            uint[] v = KonverterReciBajtovi.BytesToWords(input, inputOffset, brojReciPoBloku);
            EncryptWords(v);
            KonverterReciBajtovi.WordsToBytes(v, output, outputOffset, brojReciPoBloku);
        }

        private void DecryptBlock(byte[] input, int inputOffset,
                             byte[] output, int outputOffset)
        {
            uint[] v = KonverterReciBajtovi.BytesToWords(input, inputOffset, brojReciPoBloku);
            DecryptWords(v);
            KonverterReciBajtovi.WordsToBytes(v, output, outputOffset, brojReciPoBloku);
        }

        private void EncryptWords(uint[] v)
        {
            int n = brojReciPoBloku;
            uint rounds = (uint)(6 + 52 / n);

            uint sum = 0;
            uint z = v[n - 1], y;

            while (rounds > 0)
            {
                sum += DELTA;
                uint e = (sum >> 2) & 3;

                for (int p = 0; p < n - 1; p++)
                {
                    y = v[p + 1];
                    z = v[p] += MX(sum, y, z, p, e);
                }

                y = v[0];
                z = v[n - 1] += MX(sum, y, z, n - 1, e);
                rounds--;
            }
        }

        private void DecryptWords(uint[] v)
        {
            int n = brojReciPoBloku;
            uint rounds = (uint)(6 + 52 / n);
            uint sum = rounds * DELTA;

            uint y = v[0], z;

            while (rounds > 0)
            {
                uint e = (sum >> 2) & 3;

                for (int p = n - 1; p > 0; p--)
                {
                    z = v[p - 1];
                    y = v[p] -= MX(sum, y, z, p, e);
                }

                z = v[n - 1];
                y = v[0] -= MX(sum, y, z, 0, e);

                sum -= DELTA;
                rounds--;
            }
        }

        private uint MX(uint sum, uint y, uint z, int p, uint e)
        {
            return ((z >> 5 ^ y << 2) +
                    (y >> 3 ^ z << 4)) ^
                   ((sum ^ y) +
                    (key[(p & 3) ^ e] ^ z));
        }

        #endregion
    }
}
