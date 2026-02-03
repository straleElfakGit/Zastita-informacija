using Hijerarhija_Algoritama;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFBMod
{
    public class CFBModSifrator : BlockModeSifrator
    {
        private byte[] shiftRegister;
        private byte[] blockOutput;
        private int sBytes;

        public CFBModSifrator(BlockSifrator<byte[]> blockSifrator, int sBits)
            : base(blockSifrator)
        {
            int blockBits = blockSifrator.VelicinaBloka * 8;
            if (sBits <= 0 || sBits > blockBits)
                throw new ArgumentException("Parametar s ne sme biti veci od velicine bloka.");

            if (sBits % 8 != 0)
                throw new ArgumentException("Parametar s mora biti umnozak od 8.");

            sBytes = sBits >> 3;
        }

        public void ResetState()
        {
            ResetInternalState();
        }

        protected override void ResetInternalState()
        { 
            shiftRegister = (byte[])iv.Clone();
            blockOutput = new byte[blockSifrator.VelicinaBloka];
        }

        public override byte EncryptByte(byte input)
        {
            throw new NotSupportedException(
                "EncryptByte nije podrzan za CFB-s gde je s > 8 bita.");
        }

        public override byte[] Encrypt(byte[] plaintext)
        {
            if (plaintext == null)
                throw new ArgumentNullException(nameof(plaintext));

            if (plaintext.Length % sBytes != 0)
                throw new ArgumentException("Duzina ulaza mora biti umnozak s.");

            byte[] ciphertext = new byte[plaintext.Length];
            int offset = 0;
            while (offset < plaintext.Length)
            {
                blockOutput = blockSifrator.EncryptBlock(shiftRegister);
                for (int i = 0; i < sBytes; i++)
                    ciphertext[offset + i] = (byte)(plaintext[offset + i] ^ blockOutput[i]);
                ShiftRegisterUpdate(ciphertext, offset);
                offset += sBytes;
            }

            return ciphertext;
        }

        public override byte[] Decrypt(byte[] ciphertext)
        {
            if (ciphertext == null)
                throw new ArgumentNullException(nameof(ciphertext));

            if (ciphertext.Length % sBytes != 0)
                throw new ArgumentException("Duzina ulaza mora biti umnozak s.");

            byte[] plaintext = new byte[ciphertext.Length];
            int offset = 0;
            while (offset < ciphertext.Length)
            {
                blockOutput = blockSifrator.EncryptBlock(shiftRegister);
                for (int i = 0; i < sBytes; i++)
                    plaintext[offset + i] = (byte)(ciphertext[offset + i] ^ blockOutput[i]);
                ShiftRegisterUpdate(ciphertext, offset);
                offset += sBytes;
            }

            return plaintext;
        }

        public byte[] DecryptInParallel(byte[] ciphertext, int parallelism = 4)
        {
            if (ciphertext == null)
                throw new ArgumentNullException(nameof(ciphertext));

            if (ciphertext.Length % sBytes != 0)
                throw new ArgumentException("Duzina ulaza mora biti umnozak s.");

            byte[] plaintext = new byte[ciphertext.Length];
            int totalBlocks = ciphertext.Length / sBytes;

            // 1. Prvo napravimo niz shiftRegistera za svaki blok
            byte[][] shiftRegisters = new byte[totalBlocks][];
            shiftRegisters[0] = (byte[])iv.Clone();

            for (int i = 1; i < totalBlocks; i++)
            {
                shiftRegisters[i] = new byte[shiftRegister.Length];
                Buffer.BlockCopy(shiftRegisters[i - 1], sBytes, shiftRegisters[i], 0, shiftRegister.Length - sBytes);
                Buffer.BlockCopy(ciphertext, (i - 1) * sBytes, shiftRegisters[i], shiftRegister.Length - sBytes, sBytes);
            }

            // 2. Paralelno generišemo blockOutput za svaki shiftRegister
            byte[][] blockOutputs = new byte[totalBlocks][];
            Parallel.For(0, totalBlocks, new ParallelOptions { MaxDegreeOfParallelism = parallelism }, i =>
            {
                blockOutputs[i] = blockSifrator.EncryptBlock(shiftRegisters[i]);
            });

            // 3. XOR sa ciphertext-om da dobijemo plaintext
            for (int i = 0; i < totalBlocks; i++)
            {
                int offset = i * sBytes;
                for (int j = 0; j < sBytes; j++)
                    plaintext[offset + j] = (byte)(ciphertext[offset + j] ^ blockOutputs[i][j]);
            }

            // 4. Update shiftRegister za eventualno dalje korišćenje
            Buffer.BlockCopy(shiftRegisters[totalBlocks - 1], 0, shiftRegister, 0, shiftRegister.Length);

            return plaintext;
        }

        private void ShiftRegisterUpdate(byte[] ciphertext, int offset)
        {
            int b = shiftRegister.Length;

            Buffer.BlockCopy(
                shiftRegister,
                sBytes,
                shiftRegister,
                0,
                b - sBytes
            );

            Buffer.BlockCopy(
                ciphertext,
                offset,
                shiftRegister,
                b - sBytes,
                sBytes
            );
        }
    }
}
