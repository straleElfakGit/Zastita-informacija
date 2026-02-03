using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hijerarhija_Algoritama
{
    public abstract class BlockModeSifrator : StreamSifrator<byte>, SifratorBytePodataka
    {
        protected readonly BlockSifrator<byte[]> blockSifrator;
        protected byte[] iv;

        protected BlockModeSifrator(BlockSifrator<byte[]> blockSifrator)
        {
            this.blockSifrator = blockSifrator
                ?? throw new ArgumentNullException(nameof(blockSifrator));
        }

        public override void Init(byte[] key, byte[] initialValue)
        {
            if (initialValue == null || initialValue.Length != blockSifrator.VelicinaBloka)
                throw new ArgumentException("IV mora imati velicinu bloka.");

            iv = (byte[])initialValue.Clone();
            ResetInternalState();
        }

        protected abstract void ResetInternalState();

        public abstract byte[] Encrypt(byte[] plaintext);
        public abstract byte[] Decrypt(byte[] ciphertext);
    }
}
