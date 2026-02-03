using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PodesavanjaAlgoritama
{
    public class CFBSettings : AlgorithmSettings
    {
        [JsonPropertyName("segment_bits_s")]
        public int SBits { get; set; } = 8;

        [JsonPropertyName("initialization_vector")]
        public byte[] IV { get; set; }

        public CFBSettings()
        {
            SBits = 8;
            IV = new byte[0];
        }

        public override bool ConsistantSettings()
        {
            if (SBits <= 1)
                return false;
            if (SBits % 8 != 0)
                return false;
            if (IV == null)
                return false;

            return true;
        }

        public static bool ConsistantSettingsStatic(CFBSettings settings)
        {
            return settings != null && settings.ConsistantSettings();
        }
    }
}
