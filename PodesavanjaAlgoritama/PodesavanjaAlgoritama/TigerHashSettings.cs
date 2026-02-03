using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PodesavanjaAlgoritama
{
    public enum PaddingStrategy
    {
        None = 0,
        SimpleZeroPadding = 1,    
        StandardMerkleDamgard = 2 
    }
    public class TigerHashSettings : AlgorithmSettings
    {
        [JsonPropertyName("padding_strategy")]
        public PaddingStrategy SelectedStrategy { get; set; } = PaddingStrategy.StandardMerkleDamgard;

        [JsonPropertyName("number_of_passes")]
        public int NumberOfPasses { get; set; } = 3;

        public TigerHashSettings()
        {
            SelectedStrategy = PaddingStrategy.StandardMerkleDamgard;
            NumberOfPasses = 3;
        }

        public override bool ConsistantSettings()
        {
            if (!Enum.IsDefined(typeof(PaddingStrategy), SelectedStrategy))
                return false;
            if (NumberOfPasses < 3)
                return false;

            return true;
        }
    }
}
