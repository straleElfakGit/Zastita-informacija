using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hijerarhija_Algoritama
{
    public abstract class StreamSifrator<TSymbol> : Sifrator
    {
        public sealed override TipSiftatora Tip => TipSiftatora.Stream;

        public abstract void Init(byte[] key, byte[] initialValue);
        public abstract TSymbol EncryptByte(TSymbol input);
    }
}
