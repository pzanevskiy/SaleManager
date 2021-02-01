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
        private IFileWatcherProvider fileWatcher;
        private IParser parser;
        private CustomTaskScheduler taskScheduler;        
        private CancellationTokenSource cancelToken;
        private Object lockObj = new Object();
        private const int _maxTaskCount = 3;

        public TaskManager()
        {
            fileWatcher = new FileWatcherProvider();
            fileWatcher.Create += StartTask;
            parser = new CSVParser();
            taskScheduler = new CustomTaskScheduler(3);
            cancelToken = new CancellationTokenSource();
        }
        
        private void StartTask(object sender, FileSystemEventArgs e)
        {          
            Task task = new Task(() => 
            {
                var orders = parser.ManualParse(e.FullPath);
                foreach(var order in orders)
                {
                    IUnitOfWork uow = new EFUnitOfWork();
                    IOrderService service = new OrderService(uow);
                    try
                    {
                        lock (lockObj)
                        {
                            service.AddOrder(order);
                            //Console.WriteLine($"{order.Product} added from file {e.FullPath}\t Task - {Task.CurrentId}");
                        }
                    }
                    finally
                    {
                        service.Dispose();
                        uow.Dispose();
                        File.Delete(e.FullPath);
                    }
                }
            },
            cancelToken.Token);
            task.Start(taskScheduler);
        }

        public void Run()
        {
            fileWatcher.Run();
        }

        public void Stop()
        {
            cancelToken.Cancel();
            fileWatcher.Stop();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    fileWatcher.Dispose();
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
