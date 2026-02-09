using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PodesavanjaAlgoritama;

namespace AlgorithmSettingsManagerr
{
    public class AlgorithmSettingsManager
    {
        public EnigmaSettings Enigma { get; private set; }
        public XXTEASettings XXTEA { get; private set; }
        public CFBSettings CFB { get; private set; }
        public TigerHashSettings Tiger { get; private set; }
        public EnigmaLibrary Library { get; private set; }
        public GeneralSettings General { get; private set; }

        public async Task LoadAllSettingsAsync()
        {
            await Task.Delay(1000);

            Enigma = await Task.Run(() => EnigmaSettingsManager.Instance.Load());
            XXTEA = await Task.Run(() => XXTEASettingsManager.Instance.Load());
            CFB = await Task.Run(() => CFBSettingsManager.Instance.Load());
            Tiger = await Task.Run(() => TigerHashSettingsManager.Instance.Load());
            Library = await Task.Run(() => EnigmaLibraryManager.Instance.Load());
            General = await Task.Run(() => GeneralSettingsManager.Instance.Load());
        }
    }
}
