using CrytoWatcher;
using Logger;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Zastita_informacija_projekat
{
    public partial class FSWUserControl : UserControl
    {
        private CryptoWatcher _cw;
        private string _kodiraniPath;

        private ConcurrentQueue<string> _filesToProcess;
        private Thread _processingThread;
        private ManualResetEvent _stopEvent;
        private bool _isRunning;

        private ConcurrentDictionary<string, DateTime> _lastProcessedTimes = new ConcurrentDictionary<string, DateTime>();

        public FSWUserControl(CryptoWatcher cw)
        {
            InitializeComponent();
            _cw = cw;
            _kodiraniPath = Path.Combine(_cw.TargetFolder, "Kodirani fajlovi");
            _filesToProcess = new ConcurrentQueue<string>();

            if (!Directory.Exists(_kodiraniPath)) 
                Directory.CreateDirectory(_kodiraniPath);

            _cw.Watcher.SynchronizingObject = this;

            _cw.Created += OnFileSystemEvent;
            _cw.Changed += OnFileSystemEvent;
            _cw.Deleted += OnFileSystemEvent;
            _cw.Renamed += OnRenamedEvent;

            _stopEvent = new ManualResetEvent(false);
            _isRunning = true;
            _processingThread = new Thread(ProcessFiles);
            _processingThread.Start();

            InicijalizujUI();
            OsveziListeFajlova();

            this.Disposed += (sender, e) => Cleanup();
        }

        private bool IsFileAvailable(string filePath)
        {
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    return true;
                }
            }
            catch (IOException)
            {
                return false;
            }
        }

        private void FSWUserControl_Load(object sender, EventArgs e)
        {
        }

        private void InicijalizujUI()
        {
            label1.Text = "Watcher: " + _cw.Name;
            label2.Text = $"Algoritam: {_cw.SelectedAlgorithm}";
            groupBox1.Text = $"Putanja: {_cw.TargetFolder}";
            btnToogle.Text = _cw.Watcher.EnableRaisingEvents ? "FSW OFF" : "FSW ON";
        }

        public void OsveziListeFajlova()
        {
            FillListView(lvOriginalni, _cw.TargetFolder);
            FillListView(listView3, _kodiraniPath);
        }

        private void FillListView(ListView lv, string path)
        {
            lv.Items.Clear();
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var file in di.GetFiles())
            {
                if (file.Attributes.HasFlag(FileAttributes.Directory)) continue;
                lv.Items.Add(new ListViewItem(file.Name));
            }
        }

        private void btnToogle_Click(object sender, EventArgs e)
        {
            _cw.Watcher.EnableRaisingEvents = !_cw.Watcher.EnableRaisingEvents;
            InicijalizujUI();
            button2.Enabled = !_cw.Watcher.EnableRaisingEvents;
            string stanje = _cw.Watcher.EnableRaisingEvents ? "ponovo uključnen." : "isključen.";
            Logger.Logger.Instance.Log($"FSW {_cw.Name} je {stanje}", LogType.Info);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvOriginalni.SelectedItems.Count > 0)
            {
                string fileName = lvOriginalni.SelectedItems[0].Text;
                string fullPath = Path.Combine(_cw.TargetFolder, fileName);

                try
                {
                    System.Diagnostics.Process.Start(fullPath);
                    Logger.Logger.Instance.Log($"Fajl {fullPath} je uspešno otvoren.", Logger.LogType.Info);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ne mogu da otvorim fajl: " + ex.Message);
                    Logger.Logger.Instance.Log($"Nije moguće otvoriti fajl: {fullPath}", Logger.LogType.Warning);
                }
            }
        }

        private void OnFileSystemEvent(object sender, FileSystemEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnFileSystemEvent(sender, e)));
                return;
            }

            if (e.ChangeType == WatcherChangeTypes.Created ||
        e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (_lastProcessedTimes.TryGetValue(e.FullPath, out DateTime lastProcessed))
                {
                    if ((DateTime.Now - lastProcessed).TotalSeconds < 2) // Чекај бар 2 секунде
                    {
                        Logger.Logger.Instance.Log($"[{_cw.Name}] Ignorišem {e.ChangeType} za {e.Name} (prebrzo nakon prethodne obrade)", LogType.Warning);
                        return;
                    }
                }
                Logger.Logger.Instance.Log($"[{_cw.Name}] {e.ChangeType}: {e.Name}", LogType.Info);

                _lastProcessedTimes[e.FullPath] = DateTime.Now;
                _filesToProcess.Enqueue(e.FullPath);
                _stopEvent.Set();

                OsveziListeFajlova();
            }
            else if (e.ChangeType == WatcherChangeTypes.Deleted)
            {
                _lastProcessedTimes.TryRemove(e.FullPath, out _);
                Logger.Logger.Instance.Log($"[{_cw.Name}] Obrisan je fajl: {e.Name}", LogType.Info);
                OsveziListeFajlova();
            }
        }

        private void OnRenamedEvent(object sender, RenamedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => OnRenamedEvent(sender, e)));
                return;
            }

            Logger.Logger.Instance.Log($"[{_cw.Name}] Renamed: {e.OldName} -> {e.Name}", LogType.Info);
            
            _filesToProcess.Enqueue(e.FullPath);
            _stopEvent.Set();

            OsveziListeFajlova();
        }

        public void Cleanup()
        {
            try
            {
                _isRunning = false;
                _stopEvent?.Set();

                if (_processingThread != null && _processingThread.IsAlive)
                {
                    if (!_processingThread.Join(3000))
                    {
                        try
                        {
                            _processingThread.Abort();
                        }
                        catch { }
                    }
                }

                _stopEvent?.Dispose();

                if (_cw != null)
                {
                    _cw.Created -= OnFileSystemEvent;
                    _cw.Changed -= OnFileSystemEvent;
                    _cw.Deleted -= OnFileSystemEvent;
                    _cw.Renamed -= OnRenamedEvent;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Log($"Greška pri čišćenju resursa za {_cw?.Name}: {ex.Message}", LogType.Error);
            }
        }

        private void ProcessFiles()
        {
            while (_isRunning)
            {
                _stopEvent.WaitOne(1000);

                while (_filesToProcess.TryDequeue(out string filePath))
                {
                    try
                    {
                        while (!IsFileAvailable(filePath) && _isRunning)
                        {
                            Thread.Sleep(500);
                        }

                        if (!_isRunning) 
                            break;
                        ProcessEncryption(filePath);
                    }
                    catch (Exception ex)
                    {
                        Logger.Logger.Instance.Log($"Greška pri obradi {Path.GetFileName(filePath)}: {ex.Message}", LogType.Error);
                    }
                }
            }
        }

        private void ProcessEncryption(string filePath)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ProcessEncryption(filePath)));
                return;
            }

            try
            {
                string fileName = Path.GetFileName(filePath);
                Logger.Logger.Instance.Log($"Počinjem enkripciju: {fileName}", LogType.Info);
                string encryptedPath = Path.Combine(_kodiraniPath, fileName + ".enc");
                OsveziListeFajlova();

                Logger.Logger.Instance.Log($"Enkripcija završena: {fileName}", LogType.Info);
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Log($"Greška pri enkripciji {Path.GetFileName(filePath)}: {ex.Message}", LogType.Error);
            }
        }
    }
}
