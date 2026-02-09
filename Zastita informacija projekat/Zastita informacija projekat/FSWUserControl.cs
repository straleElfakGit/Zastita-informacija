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
        private ICryptoProcessor _cryptoProcessor;

        private ConcurrentQueue<FileOperation> _filesToProcess;
        private Thread _processingThread;
        private ManualResetEvent _stopEvent;
        private bool _isRunning;

        private ConcurrentDictionary<string, DateTime> _lastProcessedTimes = new ConcurrentDictionary<string, DateTime>();

        private struct FileOperation
        {
            public string FilePath { get; set; }
            public CryptoOperation Operation { get; set; }
            public WatcherChangeTypes ChangeType { get; set; }
        }

        public FSWUserControl(CryptoWatcher cw)
        {
            InitializeComponent();
            _cw = cw;
            _cryptoProcessor = cw.CryptoProcessor;
            _kodiraniPath = Path.Combine(_cw.TargetFolder, "Kodirani fajlovi");
            _filesToProcess = new ConcurrentQueue<FileOperation>();

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
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(OsveziListeFajlova));
                return;
            }

            FillListView(lvOriginalni, _cw.TargetFolder);
            FillListView(listView3, _kodiraniPath);
        }

        private void FillListView(ListView lv, string path)
        {
            lv.Items.Clear();
            if (!Directory.Exists(path))
                return;
            DirectoryInfo di = new DirectoryInfo(path);
            foreach (var file in di.GetFiles())
            {
                if (file.Attributes.HasFlag(FileAttributes.Directory)) continue;
                var item = new ListViewItem(file.Name);
                item.SubItems.Add(FormatFileSize(file.Length));
                item.SubItems.Add(file.LastWriteTime.ToString("dd.MM.yyyy HH:mm"));
                item.Tag = file.FullName;

                lv.Items.Add(item);
            }
        }

        private string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
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

            if (e.FullPath.StartsWith(_kodiraniPath))
                return;

            if (e.ChangeType == WatcherChangeTypes.Created ||
        e.ChangeType == WatcherChangeTypes.Changed)
            {
                if (_lastProcessedTimes.TryGetValue(e.FullPath, out DateTime lastProcessed))
                {
                    if ((DateTime.Now - lastProcessed).TotalSeconds < 2)
                    {
                        Logger.Logger.Instance.Log($"[{_cw.Name}] Ignorišem {e.ChangeType} za {e.Name} (prebrzo nakon prethodne obrade)", LogType.Warning);
                        return;
                    }
                }
                Logger.Logger.Instance.Log($"[{_cw.Name}] {e.ChangeType}: {e.Name}", LogType.Info);

                _lastProcessedTimes[e.FullPath] = DateTime.Now;
                _filesToProcess.Enqueue(new FileOperation
                {
                    FilePath = e.FullPath,
                    Operation = CryptoOperation.Encrypt,
                    ChangeType = e.ChangeType
                });
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

            if (e.FullPath.StartsWith(_kodiraniPath))
                return;

            Logger.Logger.Instance.Log($"[{_cw.Name}] Renamed: {e.OldName} -> {e.Name}", LogType.Info);
            
            _filesToProcess.Enqueue(new FileOperation
            {
                FilePath = e.FullPath,
                Operation = CryptoOperation.Encrypt,
                ChangeType = WatcherChangeTypes.Renamed
            });
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

                while (_filesToProcess.TryDequeue(out FileOperation operation))
                {
                    try
                    {
                        while (!IsFileAvailable(operation.FilePath) && _isRunning)
                        {
                            Thread.Sleep(500);
                        }

                        if (!_isRunning) 
                            break;
                        ProcessFileOperation(operation);
                    }
                    catch (Exception ex)
                    {
                        Logger.Logger.Instance.Log($"Greška pri obradi {Path.GetFileName(operation.FilePath)}: {ex.Message}", LogType.Error);
                    }
                }
            }
        }

        private void ProcessFileOperation(FileOperation operation)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => ProcessFileOperation(operation)));
                return;
            }

            try
            {
                string fileName = Path.GetFileName(operation.FilePath);
                if (operation.Operation == CryptoOperation.Encrypt)
                    ProcessEncryption(operation.FilePath);
                else if (operation.Operation == CryptoOperation.Decrypt)
                {
                    //ProcessDecryption(operation.FilePath);
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Log($"Greška pri obradi {Path.GetFileName(operation.FilePath)}: {ex.Message}", LogType.Error);
            }
        }

        private void ProcessEncryption(string filePath)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                Logger.Logger.Instance.Log($"Počinjem enkripciju: {fileName}", LogType.Info);

                string encryptedFileName = fileName + ".enc";
                string encryptedPath = Path.Combine(_kodiraniPath, encryptedFileName);

                if (_cryptoProcessor != null && _cryptoProcessor.CanProcessFile(filePath))
                {
                    _cryptoProcessor.EncryptFile(filePath, encryptedPath);
                    Logger.Logger.Instance.Log($"Enkripcija završena: {fileName}", LogType.Info);
                }
                else
                {
                    Logger.Logger.Instance.Log($"Nije moguće obraditi fajl {fileName}", LogType.Warning);
                }

                OsveziListeFajlova();
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Log($"Greška pri enkripciji {Path.GetFileName(filePath)}: {ex.Message}", LogType.Error);
            }
        }

        private void ProcessDecryption(string filePath, string outputPath = null)
        {
            try
            {
                string fileName = Path.GetFileName(filePath);
                Logger.Logger.Instance.Log($"Počinjem dekripciju: {fileName}", LogType.Info);

                string decryptedFileName = Path.GetFileNameWithoutExtension(fileName);

                string decryptedPath = outputPath ?? Path.Combine(_cw.TargetFolder, decryptedFileName);

                if (_cryptoProcessor != null && _cryptoProcessor.CanProcessFile(filePath))
                {
                    _cryptoProcessor.DecryptFile(filePath, decryptedPath);
                    Logger.Logger.Instance.Log($"Dekripcija završena: {fileName} -> {decryptedFileName}", LogType.Info);
                }
                else
                {
                    Logger.Logger.Instance.Log($"Nije moguće obraditi fajl {fileName}", LogType.Warning);
                }

                OsveziListeFajlova();
            }
            catch (Exception ex)
            {
                Logger.Logger.Instance.Log($"Greška pri dekripciji {Path.GetFileName(filePath)}: {ex.Message}", LogType.Error);
            }
        }

        private void listView3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView3.SelectedItems.Count > 0)
            {
                var item = listView3.SelectedItems[0];
                string filePath = item.Tag as string;

                if (MessageBox.Show($"Da li želite da dekriptujete fajl {item.Text} u originalni folder?",
                    "Dekripcija", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    _filesToProcess.Enqueue(new FileOperation
                    {
                        FilePath = filePath,
                        Operation = CryptoOperation.Decrypt,
                        ChangeType = WatcherChangeTypes.Changed
                    });
                    _stopEvent.Set();
                }
            }
        }
    }
}
