using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal class TigerPaddingSaDuzinomPoruke : ITigerPaddingStrategy
    {
        byte[] ITigerPaddingStrategy.Pad(byte[] message, ulong totalLength)
        {
            return InternalPad(message, totalLength);
        }

        public byte[] Pad(byte[] message, ulong totalLength)
        {
            return InternalPad(message, totalLength);
        }

        private byte[] InternalPad(byte[] message, ulong totalLength)
        {
            ulong bitLen = totalLength * 8;
            var padded = new List<byte>(message);

            padded.Add(0x01);

            while (padded.Count % 64 != 56)
                padded.Add(0x00);

            padded.AddRange(BitConverter.GetBytes(bitLen));

            return padded.ToArray();
        }
    }
}
