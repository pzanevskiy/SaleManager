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
using System.Configuration;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace SaleManager.BL.TaskManager
{
    public class TaskManager : ITaskManager
    {
        private IFileWatcherProvider _fileWatcher;
        IDirectoryHandler _directoryHandler;
        private IParser _parser;
        private CustomTaskScheduler _taskScheduler;
        private CancellationTokenSource _cancelToken;
        private Logger _logger;
        private Object _lockObj = new Object();
        private const int _maxTaskCount = 3;

        public TaskManager()
        {
            _fileWatcher = new FileWatcherProvider();
            _fileWatcher.Create += StartTask;
            _directoryHandler = new DirectoryHandler();
            _parser = new CSVParser();
            _taskScheduler = new CustomTaskScheduler(_maxTaskCount);
            _cancelToken = new CancellationTokenSource();
            _logger = new Logger();
        }

        private void StartTask(object sender, FileSystemEventArgs e)
        {
            Task task = new Task(() =>
            {
                var orders = _parser.ManualParse(e.FullPath);
                foreach (var order in orders)
                {
                    Thread.Sleep(50);
                    IUnitOfWork uow = new EFUnitOfWork();
                    IOrderService service = new OrderService(uow);
                    try
                    {
                        lock (_lockObj)
                        {
                            if (!_cancelToken.IsCancellationRequested)
                            {
                                service.AddOrder(order);
                                _logger.Info($"{order.Product} added from file {e.Name}\t Task - {Task.CurrentId}");
                            }
                            else
                            {
                                _logger.Info("Task stopped");
                                break;
                            }
                        }
                    }
                    catch(Exception)
                    {
                        _logger.Info("Can't add record to database");
                        throw new InvalidOperationException("Can't add record to database");
                    }
                    finally
                    {
                        service.Dispose();
                        uow.Dispose();
                    }
                }
                if (!_cancelToken.IsCancellationRequested)
                {
                    _directoryHandler.Move(e.FullPath, e.Name);
                }
            },
            _cancelToken.Token);
            task.Start(_taskScheduler);
        }

        public void Run()
        {
            _fileWatcher.Run();
        }

        public void Stop()
        {
            _cancelToken.Cancel();
            _fileWatcher.Stop();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _fileWatcher.Dispose();
                    _cancelToken.Dispose();
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
