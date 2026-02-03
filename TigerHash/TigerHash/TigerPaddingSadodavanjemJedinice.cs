using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal class TigerPaddingSadodavanjemJedinice : ITigerPaddingStrategy
    {
        byte[] ITigerPaddingStrategy.Pad(byte[] message, ulong totalLength)
        {
            return InternalPad(message);
        }

        public byte[] Pad(byte[] message)
        {
            return InternalPad(message);
        }

        private byte[] InternalPad(byte[] message)
        {
            var padded = new List<byte>(message);

            while ((padded.Count * 8) % 512 != 0)
                padded.Add(0xFF);

            return padded.ToArray();
        }
    }
}
