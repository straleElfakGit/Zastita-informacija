using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrytoWatcher
{
    public interface IProgressReporter
    {
        void ReportProgress(string fileName, long bytesProcessed, long totalBytes);
        void ReportCompleted(string fileName);
        void ReportError(string fileName, string errorMessage);
    }
}
