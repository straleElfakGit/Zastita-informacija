using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    public class TigerHash
    {
        private readonly ITigerBlockProcesor _processor;
        private readonly ITigerPaddingStrategy _padding;
        private readonly TigerKontekst _context;

        private byte[] _currentBlock = new byte[64];
        private int _bytesInBlock = 0;
        private ulong _totalBytesProcessed = 0;
        public TigerHash(int stategija)
        {
            ISBox sbox = new FileSBox("sbox.txt");
            _processor = new TigerBlockProcesor(sbox);
            _context = new TigerKontekst();
            _padding = new TigerPaddingSaDuzinomPoruke();
        }

        public void Update(byte[] data, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                _currentBlock[_bytesInBlock++] = data[offset + i];
                _totalBytesProcessed++;

                if (_bytesInBlock == 64)
                {
                    _processor.ProcessBlock(_currentBlock, _context);
                    _bytesInBlock = 0;
                }
            }
        }

        public byte[] FinalizeHash()
        {
            byte[] finalBytes = new byte[_bytesInBlock];
            Array.Copy(_currentBlock, 0, finalBytes, 0, _bytesInBlock);

            var padded = _padding.Pad(finalBytes, _totalBytesProcessed);
            for (int i = 0; i < padded.Length; i += 64)
            {
                byte[] block = new byte[64];
                Array.Copy(padded, i, block, 0, 64);
                _processor.ProcessBlock(block, _context);
            }

            return BitConverter.GetBytes(_context.H0)
                .Concat(BitConverter.GetBytes(_context.H1))
                .Concat(BitConverter.GetBytes(_context.H2))
                .ToArray();
        }

        /*public byte[] ComputeHash(byte[] message)
        {
            var padded = _padding.Pad(message);

            for (int i = 0; i < padded.Length; i += 64)
            {
                var block = new byte[64];
                Array.Copy(padded, i, block, 0, 64);
                _processor.ProcessBlock(block, _context);
            }

            return BitConverter.GetBytes(_context.H0)
                .Concat(BitConverter.GetBytes(_context.H1))
                .Concat(BitConverter.GetBytes(_context.H2))
                .ToArray();
        }*/

        public void ResetContext() 
        { 
            _context.Reset();
            _bytesInBlock = 0;
            _totalBytesProcessed = 0;
        }
    }
}
