using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PodesavanjaAlgoritama
{
    public class EnigmaSettings : AlgorithmSettings
    {
        [JsonPropertyName("block_size_n")]
        public int BlockSize { get; set; } = 26;

        [JsonPropertyName("rotor_count")]
        public int RotorCount => PermutacijeRotora?.Length ?? 0;

        [JsonPropertyName("rotor_wirings")]
        public int[][] PermutacijeRotora { get; set; }

        [JsonPropertyName("rotor_notches")]
        public int[] NotcheviRotora { get; set; }

        [JsonPropertyName("key_settings")]
        public int[] KeySettings { get; set; }

        [JsonPropertyName("ring_settings")]
        public int[] RingSettings { get; set; }

        [JsonPropertyName("reflector_map")]
        public int[] Reflektor { get; set; }

        [JsonPropertyName("plugboard_pairs")]
        public int[] PlugBoard { get; set; }

        public EnigmaSettings()
        {
            InitializeDefault(26, 3);
        }

        private void InitializeDefault(int n, int count)
        {
            BlockSize = n;
            PermutacijeRotora = new int[count][];
            for (int i = 0; i < count; i++)
                PermutacijeRotora[i] = new int[n];

            NotcheviRotora = new int[count];
            KeySettings = new int[count];
            RingSettings = new int[count];
            Reflektor = new int[n];
            PlugBoard = new int[n];
        }

        public override bool ConsistantSettings()
        {
            return EnigmaSettings.ConsistantSettignsStatic(this);
        }

        public static bool ConsistantSettignsStatic(EnigmaSettings es)
        {
            if (es == null || es.PermutacijeRotora == null) 
                return false;
            if (es.Reflektor.Length != es.BlockSize || es.PlugBoard.Length != es.BlockSize)
                return false;
            int count = es.PermutacijeRotora.Length;
            for (int i = 0; i < count; i++)
            {
                if (es.PermutacijeRotora[i] == null || es.PermutacijeRotora[i].Length != es.BlockSize)
                    return false;
            }
            if (es.NotcheviRotora.Length != count ||
                es.KeySettings.Length != count ||
                es.RingSettings.Length != count)
                return false;
            return true;
        }
    }
}
