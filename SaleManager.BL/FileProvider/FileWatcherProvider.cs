using SaleManager.BL.FileProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace SaleManager.BL.FileProvider
{
    public class FileWatcherProvider : IFileWatcherProvider
    {
        private Logger _logger;
        private string _fileNamePattern;
        public FileSystemWatcher Watcher { get; private set; }
        public event EventHandler<FileSystemEventArgs> Create;
        
        public FileWatcherProvider()
        {
            _logger = new Logger();
            Watcher = new FileSystemWatcher(
                ConfigurationManager.AppSettings["sourceFolder"], 
                ConfigurationManager.AppSettings["filePattern"]
                );
            Watcher.Created += OnCreated;
            _fileNamePattern = ConfigurationManager.AppSettings["fileNamePattern"];
        }
        
        protected void OnCreated(object sender, FileSystemEventArgs e)
        {
            if(IsMatch(e.Name))
            {
                _logger.Info($"{e.Name} matches pattern");
                Create?.Invoke(this, e);
            }
            else
            {
                _logger.Info($"{e.Name} do not matches pattern");
            }
        }

        public void Run()
        {
            if (Watcher != null)
            {
                Watcher.EnableRaisingEvents = true;
            }
        }

        public void Stop()
        {
            if (Watcher != null)
            {
                Watcher.EnableRaisingEvents = false;
            }
        }

        private bool IsMatch(string fileName)
        {
            return Regex.IsMatch(fileName, _fileNamePattern);
        }

        public void Dispose()
        {
            if (Watcher != null)
            {
                Watcher.Created -= OnCreated;
                Watcher.Dispose();
                GC.SuppressFinalize(this);
                Watcher = null;
            }
        }

        ~FileWatcherProvider()
        {
            Dispose();
        }

    }
}
