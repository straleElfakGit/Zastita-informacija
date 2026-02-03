using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace PodesavanjaAlgoritama
{
    public abstract class JsonSettingsProvider<T, TInstance> : ISettingsManager<T>
    where T : AlgorithmSettings, new()
    where TInstance : class, new()
    {
        protected abstract string FileName { get; }
        private static TInstance _instance;
        private static readonly object _lock = new object();

        public static TInstance Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null) _instance = new TInstance();
                    return _instance;
                }
            }
        }

        public void Save(T settings)
        {
            string directory = Path.GetDirectoryName(FileName);
            if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);

            if (settings.ConsistantSettings())
            {
                string json = JsonSerializer.Serialize(settings, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FileName, json);
            }
        }

        public T Load()
        {
            if (!File.Exists(FileName)) return new T();

            string json = File.ReadAllText(FileName);
            return JsonSerializer.Deserialize<T>(json) ?? new T();
        }
    }
}
