using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace FileMetaPodaci
{
    public static class MetadataService
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        public static bool SaveHeader(FileMetadata data, string filePath)
        {
            try
            {
                data.Timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string jsonString = JsonSerializer.Serialize(data, _options);
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
