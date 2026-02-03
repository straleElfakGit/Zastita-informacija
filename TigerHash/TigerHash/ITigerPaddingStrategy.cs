using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal interface ITigerPaddingStrategy
    {
        byte[] Pad(byte[] message, ulong totalLength);
    }
}
