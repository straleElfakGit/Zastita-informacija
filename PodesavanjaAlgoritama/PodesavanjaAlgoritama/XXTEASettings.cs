using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PodesavanjaAlgoritama
{
    public class XXTEASettings : AlgorithmSettings
    {
        [JsonPropertyName("key_128bit")]
        public uint[] Key { get; set; } = new uint[4] { 0, 0, 0, 0 };

        [JsonPropertyName("words_per_block")]
        public int BrojReciPoBloku { get; set; } = 2;

        public XXTEASettings()
        {
            Key = new uint[] { 0, 0, 0, 0 };
            BrojReciPoBloku = 2;
        }

        public override bool ConsistantSettings()
        {
            if (Key == null || Key.Length != 4)            
                return false;

            if (BrojReciPoBloku < 2)       
                return false;
            return true;
        }

        public static bool ConsistantSettingsStatic(XXTEASettings settings)
        {
            return settings != null && settings.ConsistantSettings();
        }
    }
}
