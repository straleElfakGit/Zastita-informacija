using System.CodeDom;

namespace Enigma
{
    public readonly struct EnigmaSymbol
    {
        public const int Pivot = 'A';

        public readonly int Osnova;
        public readonly int Pomeraj;

        public EnigmaSymbol(int codePoint)
        {
            int delta = codePoint - Pivot;
            Pomeraj = ((delta % 26) + 26) % 26;
            Osnova = codePoint - Pomeraj;
        }

        public int ToCodePoint(int noviPomeraj)
        {
            return Osnova + noviPomeraj;
        }

        public override string ToString()
        {
            return "Osnova: " + Osnova + ", Pomeraj: " + Pomeraj;
        }
    }

    public readonly struct GeneralniEnigmaSymbol
    {
        public readonly int Pivot;
        public readonly int Osnova;
        public readonly int Pomeraj;
        public readonly int N;

        public GeneralniEnigmaSymbol(int codePoint, int blockSize, int pivot)
        {
            N = blockSize;
            Pivot = pivot;

            int delta = codePoint - Pivot;
            Pomeraj = ((delta % N) + N) % N;
            Osnova = codePoint - Pomeraj;
        }

        public GeneralniEnigmaSymbol(int codePoint) : this(codePoint, 26, 'A') { }

        public int ToCodePoint(int noviPomeraj)
        {
            return Osnova + noviPomeraj;
        }

        public override string ToString()
        {
            return "Osnova: " + Osnova + ", Pomeraj: " + Pomeraj + ", N: " + N;
        }
    }
}