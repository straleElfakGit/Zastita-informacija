using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hijerarhija_Algoritama
{
    public interface ISifratorTextPodataka : ISifratorTipaPodatak
    {
        string Encrypt(string plaintext);
        string Decrypt(string ciphertext);

        void ResetState();
    }
}
