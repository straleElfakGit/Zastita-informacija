using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PodesavanjaAlgoritama
{
    public enum LoggingFiles
    {
        None = 0,
        Daily = 1,
        Weekly = 2,
        Monthly = 3,
        OnlyOneFile = 4
    }
    public class GeneralSettings : AlgorithmSettings
    {
        [JsonPropertyName("logging_files_frequence")]
        public LoggingFiles LoggingFilesFrequence{ get; set; } = LoggingFiles.Daily;

        public GeneralSettings() 
        {
            LoggingFilesFrequence = LoggingFiles.Daily;
        }

        public override bool ConsistantSettings()
        {
            if (!Enum.IsDefined(typeof(LoggingFiles), LoggingFilesFrequence))
                return false;

            return true;
        }
    }
}
