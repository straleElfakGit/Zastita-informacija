using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytoWatcher
{
    public enum CryptoAlgorithm { Enigma, XXTEA, CFB }

    public class CryptoWatcher
    {
        public string Name { get; set; }
        public FileSystemWatcher Watcher { get; private set; }
        public CryptoAlgorithm SelectedAlgorithm { get; set; }
        public string TargetFolder { get; set; }

        public event FileSystemEventHandler Created;
        public event FileSystemEventHandler Changed;
        public event FileSystemEventHandler Deleted;
        public event RenamedEventHandler Renamed;

        public CryptoWatcher(string name, string path, CryptoAlgorithm algo)
        {
            this.Name = name;
            this.TargetFolder = path;
            this.SelectedAlgorithm = algo;

            this.Watcher = new FileSystemWatcher(path);

            this.Watcher.Created += (sender, e) => Created?.Invoke(sender, e);
            this.Watcher.Changed += (sender, e) => Changed?.Invoke(sender, e);
            this.Watcher.Deleted += (sender, e) => Deleted?.Invoke(sender, e);
            this.Watcher.Renamed += (sender, e) => Renamed?.Invoke(sender, e);
        }

        public void Start() => Watcher.EnableRaisingEvents = true;
        public void Stop() => Watcher.EnableRaisingEvents = false;
    }
}
