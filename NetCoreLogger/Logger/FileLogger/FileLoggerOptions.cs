using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreLogger
{
    public class FileLoggerOptions
    {
        string fFileName;
        string fFolder;
        int fMaxFileSizeInMB;
        int fRetainPolicyFileCount;

        public FileLoggerOptions()
        {
        }

        public LogLevel LogLevel { get; set; } = Microsoft.Extensions.Logging.LogLevel.Information;

        public string Folder
        {
            get
            {
                return !string.IsNullOrWhiteSpace(fFolder) ?
                fFolder : System.IO.Path.GetDirectoryName(this.GetType().Assembly.Location);
            }
            set { fFolder = value; }
        }
        public string FileName
        {
            get
            {
                return !string.IsNullOrEmpty(fFileName) ? $"{fFileName}{ DateTime.UtcNow.ToString("yyyyMMddhhmm")}.log" : $"Logfile{DateTime.UtcNow.ToString("yyyyMMddhhmm")}.log";
            }
            set { fFileName = value; }
        }

        public int MaxFileSizeInMB
        {
            get { return fMaxFileSizeInMB > 0 ? fMaxFileSizeInMB : 2; }
            set { fMaxFileSizeInMB = value; }
        }

        public int RetainPolicyFileCount
        {
            get { return fRetainPolicyFileCount < 5 ? 5 : fRetainPolicyFileCount; }
            set { fRetainPolicyFileCount = value; }
        }
    }
}
