using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodesavanjaAlgoritama;

namespace ProbaPodesavanja
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string path = Path.GetDirectoryName("Settings\\enigma_settings.json");
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                Console.WriteLine("Učitavam podešavanja...");
                var settings = EnigmaSettingsManager.Instance.Load();

                if (settings.PermutacijeRotora != null)
                {
                    Console.WriteLine($"Učitano uspešno! Broj rotora: {settings.RotorCount}");
                    Console.WriteLine($"Veličina bloka: {settings.BlockSize}");

                    bool isOk = settings.ConsistantSettings();
                    Console.WriteLine($"Konzistentnost: {isOk}");
                }
                else
                {
                    Console.WriteLine("Fajl nije pronađen ili je prazan, generisan je default.");
                }

                settings.KeySettings[0] = 5;
                EnigmaSettingsManager.Instance.Save(settings);
                Console.WriteLine("Izmene sačuvane u Settings\\enigma_settings.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Greška: {ex.Message}");
            }

            /*try
            {
                Console.WriteLine("--- Testiranje XXTEA Podešavanja ---");

                // 1. Inicijalizacija: Pravimo objekte sa nekim vrednostima
                // Recimo da korisnik želi ključ od 4 uint-a i blok od 4 reči (128 bita blok)
                XXTEASettings novaPodesavanja = new XXTEASettings
                {
                    Key = new uint[] { 0xDEADBEEE, 0xCAFEBABE, 0x12345678, 0x9ABCDEF0 },
                    BrojReciPoBloku = 4
                };

                // 2. Provera konzistentnosti pre snimanja
                if (novaPodesavanja.ConsistantSettings())
                {
                    Console.WriteLine("Podešavanja su validna. Snimam u fajl...");
                    XXTEASettingsManager.Instance.Save(novaPodesavanja);
                    Console.WriteLine("Fajl uspešno sačuvan u Settings\\xxtea_settings.json");
                }
                else
                {
                    Console.WriteLine("Greška: Podešavanja nisu konzistentna!");
                }

                Console.WriteLine("\n--- Provera učitavanja ---");

                // 3. Učitavanje iz fajla
                XXTEASettings ucitano = XXTEASettingsManager.Instance.Load();

                if (ucitano != null)
                {
                    Console.WriteLine($"Učitan broj reči po bloku: {ucitano.BrojReciPoBloku}");
                    Console.WriteLine($"Prvi deo ključa (Hex): {ucitano.Key[0]:X}");

                    // Finalna potvrda
                    if (ucitano.ConsistantSettings())
                        Console.WriteLine("Učitana podešavanja su ispravna i spremna za algoritam.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Došlo je do greške: {ex.Message}");
            }*/

            /*var novaPodesavanja = new CFBSettings
            {
                SBits = 16, // Validno: >1 i deljivo sa 8
                IV = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 }
            };

            if (novaPodesavanja.ConsistantSettings())
            {
                CFBSettingsManager.Instance.Save(novaPodesavanja);
            }

            // Učitavanje
            var ucitano = CFBSettingsManager.Instance.Load();
            Console.WriteLine($"Podešen sBits: {ucitano.SBits}");
            for (int i = 0; i < ucitano.IV.Length; i++)
                Console.WriteLine(ucitano.IV[i]);*/

            /*var settings = TigerHashSettingsManager.Instance.Load();

            switch (settings.SelectedStrategy)
            {
                case PaddingStrategy.SimpleZeroPadding:
                    Console.WriteLine("Implementacija dodavanja samo nula");
                    break;

                case PaddingStrategy.StandardMerkleDamgard:
                    Console.WriteLine(" Implementacija 448 mod 512 + 64 bita za dužinu");
                    break;

                default:
                    throw new Exception("Nepoznata strategija paddinga!");
            }*/



            Console.ReadKey();
        }
    }
}
