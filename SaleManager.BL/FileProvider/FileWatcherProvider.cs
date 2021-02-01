using SaleManager.BL.FileProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SaleManager.BL.FileProvider
{
    public class FileWatcherProvider : IFileWatcherProvider
    {
        public FileSystemWatcher Watcher { get; private set; }
        public event EventHandler<FileSystemEventArgs> Create;
        
        public FileWatcherProvider()
        {           
            Watcher = new FileSystemWatcher(
                ConfigurationManager.AppSettings["sourceFolder"], 
                ConfigurationManager.AppSettings["filePattern"]
                );
            Watcher.Created += OnCreated;
        }
        
        protected void OnCreated(object sender, FileSystemEventArgs e)
        {
            Create?.Invoke(this, e);
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
