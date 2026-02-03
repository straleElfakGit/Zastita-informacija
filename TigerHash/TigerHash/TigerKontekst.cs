using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal class TigerKontekst
    {
        public ulong H0 { get; private set; }
        public ulong H1 { get; private set; }
        public ulong H2 { get; private set; }

        public TigerKontekst()
        {
            Reset();
        }

        public void Reset()
        {
            H0 = 0x0123456789ABCDEF;
            H1 = 0xFEDCBA9876543210;
            H2 = 0xF096A5B4C3D2E187;
        }

        public void Add(ulong a, ulong b, ulong c)
        {
            H0 += a;
            H1 += b;
            H2 += c;
        }
    }
}
