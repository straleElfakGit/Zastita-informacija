using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSA
{
    public class RSACipher
    {
        private readonly uint _exponent;
        private readonly ulong _n;

        public RSACipher(uint exponent, ulong n)
        {
            _exponent = exponent;
            _n = n;
        }

        public ulong[] Encrypt(byte[] data)
        {
            if (data == null) 
                return null;

            ulong[] encryptedData = new ulong[data.Length];

            for (int i = 0; i < data.Length; i++)
                encryptedData[i] = RSAHelper.Compute((ulong)data[i], _exponent, _n);
            

            return encryptedData;
        }

        public byte[] Decrypt(ulong[] encryptedData)
        {
            if (encryptedData == null) return null;

            byte[] decryptedData = new byte[encryptedData.Length];

            for (int i = 0; i < encryptedData.Length; i++)
            {
                ulong result = RSAHelper.Compute(encryptedData[i], _exponent, _n);
                decryptedData[i] = (byte)result;
            }

            return decryptedData;
        }
    }
}
