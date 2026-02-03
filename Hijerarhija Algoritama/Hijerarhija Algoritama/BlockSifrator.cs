using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hijerarhija_Algoritama
{
    public abstract class BlockSifrator<TBlock> : Sifrator
    {
        public sealed override TipSiftatora Tip => TipSiftatora.Block;

        public abstract int VelicinaBloka { get; }
        public abstract TBlock EncryptBlock(TBlock block);
    }
}
