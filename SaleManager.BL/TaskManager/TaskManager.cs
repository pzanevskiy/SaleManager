using SaleManager.BL.FileProvider;
using SaleManager.BL.FileProvider.Interfaces;
using SaleManager.BL.FileService;
using SaleManager.BL.FileService.Interfaces;
using SaleManager.BL.Service;
using SaleManager.BL.Service.Interfaces;
using SaleManager.BL.TaskManager.Interfaces;
using SaleManager.DAL;
using SaleManager.DAL.UnitOfWork.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaleManager.BL.TaskManager
{
    public class TaskManager : ITaskManager
    {
        private IFileWatcherProvider FileWatcher { get; }
        private IParser Parser { get; }
        private CustomTaskScheduler _taskScheduler;

        private CancellationTokenSource cancelToken;
        private Object lockObj = new Object();
        private const int _maxTaskCount = 3;

        public TaskManager()
        {
            FileWatcher = new FileWatcherProvider();
            FileWatcher.Create += StartTask;
            Parser = new CSVParser();
            _taskScheduler = new CustomTaskScheduler(_maxTaskCount);
            cancelToken = new CancellationTokenSource();
        }

        private void StartTask(object sender, FileSystemEventArgs e)
        {
            Task task = new Task(() => 
            {
                var orders = Parser.Parse(e.FullPath);
                foreach(var order in orders)
                {
                    IUnitOfWork uow = new EFUnitOfWork();
                    IOrderService service = new OrderService(uow);
                    try
                    {
                        lock (lockObj)
                        {
                            service.AddOrder(order);
                            Console.WriteLine($"{order.Product} added from file {e.FullPath}\t Task - {Task.CurrentId}");
                        }
                    }
                    finally
                    {
                        service.Dispose();
                        uow.Dispose();
                    }
                }
            },cancelToken.Token);
            task.Start(_taskScheduler);
        }

        public void Run()
        {
            FileWatcher.Run();
        }

        public void Stop()
        {
            cancelToken.Cancel();
            FileWatcher.Stop();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    FileWatcher.Dispose();
                    cancelToken.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
