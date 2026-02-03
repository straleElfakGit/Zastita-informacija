using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal interface ISBox
    {
        ulong[] S0 { get; }
        ulong[] S1 { get; }
        ulong[] S2 { get; }
        ulong[] S3 { get; }
    }
}
