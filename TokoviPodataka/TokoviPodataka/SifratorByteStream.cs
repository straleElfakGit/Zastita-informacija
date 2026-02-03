using Hijerarhija_Algoritama;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokoviPodataka
{
    public class SifratorByteStream : Stream
    {
        private readonly Stream _baseStream;
        private readonly SifratorBytePodataka _byteSifrator;
        private readonly bool _readMode;

        public SifratorByteStream(Stream baseStream, SifratorBytePodataka sifrator, bool readMode)
        {
            _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
            _byteSifrator = sifrator ?? throw new ArgumentNullException(nameof(sifrator));
            _readMode = readMode;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!_readMode)
                throw new NotSupportedException("Stream je u Write modu.");

            byte[] rawBytes = new byte[count];
            int bytesRead = _baseStream.Read(rawBytes, 0, count);

            if (bytesRead == 0)
                return 0;

            byte[] dataToTransform;
            if (bytesRead < count)
            {
                dataToTransform = new byte[bytesRead];
                Array.Copy(rawBytes, dataToTransform, bytesRead);
            }
            else
            {
                dataToTransform = rawBytes;
            }

            byte[] transformedBytes = _byteSifrator.Decrypt(dataToTransform);

            int finalCount = Math.Min(transformedBytes.Length, count);
            Array.Copy(transformedBytes, 0, buffer, offset, finalCount);

            return finalCount;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_readMode)
                throw new NotSupportedException("Stream je u Read modu.");

            byte[] dataToWrite = new byte[count];
            Array.Copy(buffer, offset, dataToWrite, 0, count);
            byte[] encryptedBytes = _byteSifrator.Encrypt(dataToWrite);

            _baseStream.Write(encryptedBytes, 0, encryptedBytes.Length);
        }

        // Standardne implementacije Stream-a (iste kao u tvojoj klasi)
        public override bool CanRead => _readMode;
        public override bool CanWrite => !_readMode;
        public override bool CanSeek => false;
        public override long Length => _baseStream.Length;
        public override long Position { get => _baseStream.Position; set => throw new NotSupportedException(); }
        public override void Flush() => _baseStream.Flush();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => _baseStream.SetLength(value);

        protected override void Dispose(bool disposing)
        {
            if (disposing) _baseStream.Dispose();
            base.Dispose(disposing);
        }
    }
}
