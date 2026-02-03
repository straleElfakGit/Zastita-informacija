using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hijerarhija_Algoritama
{
    public abstract class Sifrator
    {
        public Sifrator() { }

        public abstract TipSiftatora Tip { get; }
    }

    public enum TipSiftatora
    {
        Stream,
        Block
    }
}
