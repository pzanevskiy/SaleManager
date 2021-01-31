using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SaleManager.BL.FileProvider.Interfaces
{
    public interface IFileWatcherProvider : IDisposable
    {
        event EventHandler<FileSystemEventArgs> Create;

        void Run();
        void Stop();
    }
}
