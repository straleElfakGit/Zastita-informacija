using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TigerHash;

namespace TokoviPodataka
{
    public class HashByteStream : Stream
    {
        private readonly Stream _baseStream;
        private readonly TigerHash.TigerHash _tiger;
        private bool _isDisposed = false;

        public HashByteStream(Stream baseStream, TigerHash.TigerHash tiger)
        {
            _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
            _tiger = tiger ?? throw new ArgumentNullException(nameof(tiger));
        }

        public byte[] GetResult()
        {
            return _tiger.FinalizeHash();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int bytesRead = _baseStream.Read(buffer, offset, count);
            if (bytesRead > 0)
            {
                _tiger.Update(buffer, offset, bytesRead);
            }
            return bytesRead;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _baseStream.Write(buffer, offset, count);
            _tiger.Update(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    _baseStream.Dispose();
                }
                _isDisposed = true;
            }
            base.Dispose(disposing);
        }

        public override bool CanRead => _baseStream.CanRead;
        public override bool CanWrite => _baseStream.CanWrite;
        public override bool CanSeek => false;
        public override long Length => _baseStream.Length;
        public override long Position { get => _baseStream.Position; set => _baseStream.Position = value; }
        public override void Flush() => _baseStream.Flush();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => _baseStream.SetLength(value);
    }
}