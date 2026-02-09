using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileMetaPodaci
{
    public enum AlgorithmType { Enigma, XXTEA, CFB }
    public enum FileType { Textual, Binary }
    public enum HashType { TigerHash }
    public class FileMetadata
    {
        public AlgorithmType Algorithm { get; set; }
        public FileType Type { get; set; }
        public HashType HashAlgorithm { get; set; } = HashType.TigerHash;
        public string Timestamp { get; set; }
        public string OriginalFileName { get; set; }
        public long FileSize { get; set; }
    }
}
