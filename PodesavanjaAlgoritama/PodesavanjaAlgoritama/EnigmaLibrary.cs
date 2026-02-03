using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodesavanjaAlgoritama
{
    public class EnigmaLibrary : AlgorithmSettings
    {
        public List<StandardRotor> Rotors { get; set; } = new List<StandardRotor>();
        public List<StandardReflector> Reflectors { get; set; } = new List<StandardReflector>();

        public override bool ConsistantSettings()
        {
            throw new NotImplementedException();
        }
    }

    public class StandardRotor
    {
        public string Name { get; set; }
        public int[] Wiring { get; set; }
        public int Notch { get; set; }
    }

    public class StandardReflector
    {
        public string Name { get; set; }
        public int[] Wiring { get; set; }
    }
}
