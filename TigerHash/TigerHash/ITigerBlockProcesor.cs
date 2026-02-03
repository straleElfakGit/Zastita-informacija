using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal interface ITigerBlockProcesor
    {
        void ProcessBlock(byte[] block512, TigerKontekst context);
    }
}
