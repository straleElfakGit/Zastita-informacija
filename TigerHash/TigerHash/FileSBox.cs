using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TigerHash
{
    internal class FileSBox : ISBox
    {
        public ulong[] S0 { get; private set; }
        public ulong[] S1 { get; private set; }
        public ulong[] S2 { get; private set; }
        public ulong[] S3 { get; private set; }

        public FileSBox(string path)
        {
            S0 = new ulong[256];
            S1 = new ulong[256];
            S2 = new ulong[256];
            S3 = new ulong[256];

            LoadFromFile(path);
        }

        private void LoadFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                int index = 0;
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    if (line.Length == 0)
                        continue;

                    if (line.StartsWith("0x") || line.StartsWith("0X"))
                        line = line.Substring(2);

                    ulong value = ulong.Parse(
                        line,
                        NumberStyles.HexNumber,
                        CultureInfo.InvariantCulture
                    );

                    if (index < 256)
                        S0[index] = value;
                    else if (index < 512)
                        S1[index - 256] = value;
                    else if (index < 768)
                        S2[index - 512] = value;
                    else if (index < 1024)
                        S3[index - 768] = value;
                    else
                        throw new InvalidOperationException("Previše S-box vrednosti u fajlu.");

                    index++;
                }

                if (index != 1024)
                    throw new InvalidOperationException(
                        "Pogrešan broj S-box vrednosti. Očekivano: 1024, učitano: " + index);
            }
        }
    }
}
