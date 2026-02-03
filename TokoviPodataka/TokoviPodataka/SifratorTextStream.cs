using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Hijerarhija_Algoritama;

namespace TokoviPodataka
{
    public class SifratorTextStream : Stream
    {
        private readonly Stream _baseStream;
        private readonly ISifratorTextPodataka _textSifrator;
        private readonly bool _readMode;
        private readonly Encoding _encoding;

        public SifratorTextStream(Stream baseStream, ISifratorTextPodataka sifrator, bool readMode, Encoding encoding = null)
        {
            _baseStream = baseStream ?? throw new ArgumentNullException(nameof(baseStream));
            _textSifrator = sifrator ?? throw new ArgumentNullException(nameof(sifrator));
            _readMode = readMode;
            _encoding = encoding ?? Encoding.UTF8;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (!_readMode) 
                throw new NotSupportedException("Stream je u Write modu.");

            _textSifrator.ResetState();

            byte[] rawBytes = new byte[count];
            int bytesRead = _baseStream.Read(rawBytes, 0, count);
            if (bytesRead == 0) 
                return 0;

            string textChunk = _encoding.GetString(rawBytes, 0, bytesRead);
            string transformedText = _textSifrator.Decrypt(textChunk);
            byte[] transformedBytes = _encoding.GetBytes(transformedText);

            int finalCount = Math.Min(transformedBytes.Length, count);
            Array.Copy(transformedBytes, 0, buffer, offset, finalCount);

            return finalCount;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (_readMode) 
                throw new NotSupportedException("Stream je u Read modu.");

            _textSifrator.ResetState();

            string textToWrite = _encoding.GetString(buffer, offset, count);
            string encryptedText = _textSifrator.Encrypt(textToWrite);
            byte[] encryptedBytes = _encoding.GetBytes(encryptedText);
            _baseStream.Write(encryptedBytes, 0, encryptedBytes.Length);
        }

        public override bool CanRead => _readMode;
        public override bool CanWrite => !_readMode;
        public override bool CanSeek => false;
        public override long Length => _baseStream.Length;
        public override long Position { get => _baseStream.Position; set => throw new NotSupportedException(); }
        public override void Flush() => _baseStream.Flush();
        public override long Seek(long offset, SeekOrigin origin) => throw new NotSupportedException();
        public override void SetLength(long value) => _baseStream.SetLength(value);
    }
}
