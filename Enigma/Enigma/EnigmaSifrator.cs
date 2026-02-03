using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hijerarhija_Algoritama;

namespace Enigma
{
    public class EnigmaSifrator : StreamSifrator<char>, ISifratorTextPodataka
    {
        private StanjeEnigme stanje;
        private PodesavanjeEnigme pocetnaPodesavanjaEnigme;

        public EnigmaSifrator(int[][] permutacijeRotora, int[] notcheviRotora, int[] keySettings, int[] ringSettings, int[] reflektor, int[] plugBoard)
        {
            pocetnaPodesavanjaEnigme = new PodesavanjeEnigme(permutacijeRotora, notcheviRotora, keySettings, ringSettings, reflektor, plugBoard);
        }

        public override char EncryptByte(char input)
        {
            throw new NotImplementedException();
        }

        public override void Init(byte[] key, byte[] initialValue)
        {
            throw new NotImplementedException();
        }

        string ISifratorTextPodataka.Decrypt(string ciphertext)
        {
            return DecrypyLocaly(ciphertext);
        }

        string ISifratorTextPodataka.Encrypt(string plaintext)
        {
            return EncripyLocaly(plaintext);
        }

        public string Encrypt(string plaintext)
        {
            return EncripyLocaly(plaintext);
        }

        public string Decrypt(string ciphertext)
        {
            return DecrypyLocaly(ciphertext);
        }

        private string EncripyLocaly(string plaintext)
        {
            return EncriptDecript(plaintext);
        }

        private string DecrypyLocaly(string ciphertext)
        {
            return EncriptDecript(ciphertext);
        }

        private string EncriptDecript(string text)
        {
            //stanje = pocetnaPodesavanjaEnigme.vratiPocetnoStanjeEnigme();
            var sb = new StringBuilder(text.Length);
            for (int i = 0; i < text.Length; i++)
            {
                EnigmaSymbol simbol = new EnigmaSymbol(text[i]);
                int rezultat = stanje.VratiSledeceSlovo(simbol.Pomeraj);
                int noviCodePoint = simbol.ToCodePoint(rezultat);
                sb.Append(char.ConvertFromUtf32(noviCodePoint));
            }
            return sb.ToString();
        }

        void ISifratorTextPodataka.ResetState()
        {
            stanje = pocetnaPodesavanjaEnigme.vratiPocetnoStanjeEnigme();
        }

        public void ResetState()
        {
            stanje = pocetnaPodesavanjaEnigme.vratiPocetnoStanjeEnigme();
        }
    }
}
