namespace PodesavanjaAlgoritama
{
    public class EnigmaSettingsManager : JsonSettingsProvider<EnigmaSettings, EnigmaSettingsManager>
    {
        protected override string FileName => "Settings\\enigma_settings.json";
    }

    public class XXTEASettingsManager : JsonSettingsProvider<XXTEASettings, XXTEASettingsManager>
    {
        protected override string FileName => "Settings\\xxtea_settings.json";
    }

    public class CFBSettingsManager : JsonSettingsProvider<CFBSettings, CFBSettingsManager>
    {
        protected override string FileName => "Settings\\cfb_settings.json";
    }

    public class TigerHashSettingsManager : JsonSettingsProvider<TigerHashSettings, TigerHashSettingsManager>
    {
        protected override string FileName => "Settings\\tiger_settings.json";
    }

    public class EnigmaLibraryManager : JsonSettingsProvider<EnigmaLibrary, EnigmaLibraryManager>
    {
        protected override string FileName => "Settings\\enigma_library.json";
    }
}