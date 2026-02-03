using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CFBMod
{
    public class IVGenerator
    {
        public static byte[] GenerisiIV(int blockSizeBytes)
        {
            byte[] iv = new byte[blockSizeBytes];

            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(iv);
            }

            return iv;
        }

        public static byte[] GenerisiIVIzNonce(byte[] nonce, byte[] key)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.None;
                aes.Key = key;

                using (ICryptoTransform encryptor = aes.CreateEncryptor())
                {
                    return encryptor.TransformFinalBlock(nonce, 0, nonce.Length);
                }
            }
        }
    }


}
