using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytoWatcher
{
    public interface ICryptoProcessor
    {
        void EncryptFile(string inputFilePath, string outputFilePath);
        void DecryptFile(string inputFilePath, string outputFilePath);
        bool CanProcessFile(string filePath);
    }

    public enum CryptoOperation
    {
        Encrypt,
        Decrypt
    }
}
