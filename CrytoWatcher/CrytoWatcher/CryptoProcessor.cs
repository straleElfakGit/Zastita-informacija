using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlgorithmSettingsManagerr;
using Enigma;
using XXTEA;
using CFBMod;
using TokoviPodataka;
using Hijerarhija_Algoritama;

namespace CrytoWatcher
{
    public class CryptoProcessor : ICryptoProcessor
    {
        private readonly CryptoAlgorithm _algorithm;
        private readonly AlgorithmSettingsManager _settingsManager;
        private readonly string[] _textExtensions;
        private IProgressReporter _progressReporter;

        public IProgressReporter ProgressReporter
        {
            get => _progressReporter;
            set => _progressReporter = value;
        }

        public CryptoProcessor(CryptoAlgorithm algorithm, AlgorithmSettingsManager settingsManager)
        {
            _algorithm = algorithm;
            _settingsManager = settingsManager;

            _textExtensions = new[] {
                ".txt", ".xml", ".json", ".csv", ".rtf",
                ".c", ".cs", ".cpp", ".h", ".java",
                ".py", ".js", ".html", ".css", ".sql",
                ".ini", ".cfg", ".config", ".yaml", ".yml",
                ".log", ".md", ".bat", ".sh"
            };
        }

        public bool CanProcessFile(string filePath)
        {
            var extension = Path.GetExtension(filePath).ToLower();

            if (_algorithm == CryptoAlgorithm.Enigma)
                return _textExtensions.Contains(extension);

            return true;
        }

        public void EncryptFile(string inputFilePath, string outputFilePath)
        {
            ProcessFileInternal(inputFilePath, outputFilePath, CryptoOperation.Encrypt);
        }

        public void DecryptFile(string inputFilePath, string outputFilePath)
        {
            ProcessFileInternal(inputFilePath, outputFilePath, CryptoOperation.Decrypt);
        }

        private void ProcessFileInternal(string inputFilePath, string outputFilePath, CryptoOperation operation)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException($"Fajl ne postoji: {inputFilePath}");

            if (!CanProcessFile(inputFilePath))
                throw new InvalidOperationException($"Algoritam {_algorithm} ne može da obradi fajl sa ekstenzijom {Path.GetExtension(inputFilePath)}");

            string outputDirectory = Path.GetDirectoryName(outputFilePath);
            if (!Directory.Exists(outputDirectory))
                Directory.CreateDirectory(outputDirectory);

            if (File.Exists(outputFilePath))
                File.Delete(outputFilePath);

            try
            {
                long fileSize = new FileInfo(inputFilePath).Length;

                using (var inputStream = new FileStream(inputFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (var outputStream = new FileStream(outputFilePath, FileMode.Create, FileAccess.Write))
                {
                    switch (_algorithm)
                    {
                        case CryptoAlgorithm.Enigma:
                            ProcessTextFile(inputStream, outputStream, operation, inputFilePath, fileSize);
                            break;
                        case CryptoAlgorithm.XXTEA:
                            ProcessXXTEAFile(inputStream, outputStream, operation, inputFilePath, fileSize);
                            break;
                        case CryptoAlgorithm.CFB:
                            ProcessCFBFile(inputStream, outputStream, operation, inputFilePath, fileSize);
                            break;
                        default:
                            throw new NotSupportedException($"Algoritam {_algorithm} nije podržan");
                    }
                }

                _progressReporter?.ReportCompleted(inputFilePath);
            }
            catch (Exception ex)
            {
                _progressReporter?.ReportError(inputFilePath, ex.Message);
                if (File.Exists(outputFilePath))
                {
                    try
                    {
                        File.Delete(outputFilePath);
                    }
                    catch { }
                }
                throw;
            }
        }

        private void ProcessTextFile(Stream input, Stream output, CryptoOperation operation,
                                    string fileName, long fileSize)
        {
            var enigmaSifrator = CreateEnigmaSifrator();
            enigmaSifrator.ResetState();

            var sifratorStream = new SifratorTextStream(
                output,
                enigmaSifrator,
                readMode: false,
                Encoding.UTF8
            );

            using (sifratorStream)
            {
                input.CopyTo(sifratorStream);
                _progressReporter?.ReportProgress(fileName, fileSize, fileSize);
            }
        }

        private void ProcessXXTEAFile(Stream input, Stream output, CryptoOperation operation,
                                     string fileName, long fileSize)
        {
            var xxteaSifrator = CreateXXTEASifrator();
            int blockSize = xxteaSifrator.VelicinaBloka;
            long totalBytesProcessed = 0;

            if (operation == CryptoOperation.Encrypt)
            {
               
                byte[] sizeBytes = BitConverter.GetBytes(fileSize);
                output.Write(sizeBytes, 0, sizeBytes.Length);

                using (var sifratorStream = new SifratorByteStream(output, xxteaSifrator, readMode: false))
                {
                    byte[] buffer = new byte[64 * 1024];
                    int bytesRead;

                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        int ostatak = bytesRead % blockSize;
                        if (ostatak != 0)
                        {
                            int padding = blockSize - ostatak;
                            Array.Clear(buffer, bytesRead, padding);
                            sifratorStream.Write(buffer, 0, bytesRead + padding);
                        }
                        else
                        {
                            sifratorStream.Write(buffer, 0, bytesRead);
                        }

                        totalBytesProcessed += bytesRead;
                        _progressReporter?.ReportProgress(fileName, totalBytesProcessed, fileSize);
                    }
                }
            }
            else
            {
                byte[] sizeBytes = new byte[8];
                input.Read(sizeBytes, 0, 8);
                long originalSize = BitConverter.ToInt64(sizeBytes, 0);

                using (var sifratorStream = new SifratorByteStream(input, xxteaSifrator, readMode: true))
                {
                    byte[] buffer = new byte[64 * 1024];
                    int bytesRead;
                    long totalBytesWritten = 0;

                    while ((bytesRead = sifratorStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (totalBytesWritten + bytesRead > originalSize)
                        {
                            int bytesToWrite = (int)(originalSize - totalBytesWritten);
                            if (bytesToWrite > 0)
                            {
                                output.Write(buffer, 0, bytesToWrite);
                                totalBytesWritten += bytesToWrite;
                            }
                        }
                        else
                        {
                            output.Write(buffer, 0, bytesRead);
                            totalBytesWritten += bytesRead;
                        }

                        _progressReporter?.ReportProgress(fileName, totalBytesWritten, originalSize);
                    }
                }
            }
        }

        private void ProcessCFBFile(Stream input, Stream output, CryptoOperation operation,
                           string fileName, long fileSize)
        {
            var cfbSifrator = CreateCFBSifrator();
            long totalBytesProcessed = 0;

            if (operation == CryptoOperation.Encrypt)
            {
                byte[] sizeBytes = BitConverter.GetBytes(fileSize);
                output.Write(sizeBytes, 0, sizeBytes.Length);

                using (var sifratorStream = new SifratorByteStream(output, cfbSifrator, readMode: false))
                {
                    byte[] buffer = new byte[64 * 1024];
                    int bytesRead;

                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        sifratorStream.Write(buffer, 0, bytesRead);
                        totalBytesProcessed += bytesRead;
                        _progressReporter?.ReportProgress(fileName, totalBytesProcessed, fileSize);
                    }
                }
            }
            else
            {
                byte[] sizeBytes = new byte[8];
                input.Read(sizeBytes, 0, 8);
                long originalSize = BitConverter.ToInt64(sizeBytes, 0);

                using (var sifratorStream = new SifratorByteStream(input, cfbSifrator, readMode: true))
                {
                    byte[] buffer = new byte[64 * 1024];
                    int bytesRead;
                    long totalBytesWritten = 0;

                    while ((bytesRead = sifratorStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        if (totalBytesWritten + bytesRead > originalSize)
                        {
                            int bytesToWrite = (int)(originalSize - totalBytesWritten);
                            if (bytesToWrite > 0)
                            {
                                output.Write(buffer, 0, bytesToWrite);
                                totalBytesWritten += bytesToWrite;
                            }
                        }
                        else
                        {
                            output.Write(buffer, 0, bytesRead);
                            totalBytesWritten += bytesRead;
                        }

                        _progressReporter?.ReportProgress(fileName, totalBytesWritten, originalSize);
                    }
                }
            }
        }

        private EnigmaSifrator CreateEnigmaSifrator()
        {
            var settings = _settingsManager.Enigma;
            var library = _settingsManager.Library;

            return new EnigmaSifrator(
                settings.PermutacijeRotora,
                settings.NotcheviRotora,
                settings.KeySettings,
                settings.RingSettings,
                settings.Reflektor,
                settings.PlugBoard
            );
        }

        private XXTEASifrator CreateXXTEASifrator()
        {
            var settings = _settingsManager.XXTEA;

            return new XXTEASifrator(
                settings.Key,
                settings.BrojReciPoBloku
            );
        }

        private CFBModSifrator CreateCFBSifrator()
        {
            var settings = _settingsManager.CFB;

            BlockSifrator<byte[]> blockSifrator = CreateXXTEASifrator();

            return new CFBModSifrator(
                blockSifrator,
                settings.SBits
            );
        }
    }
}
