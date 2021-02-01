using SaleManager.DAL;
using SaleManager.DAL.UnitOfWork.Interfaces;
using SaleManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SaleManager.BL.FileService;
using SaleManager.BL.Service;
using SaleManager.BL.FileService.Interfaces;
using SaleManager.BL.TaskManager.Interfaces;
using SaleManager.BL.TaskManager;

namespace SaleManager.ConsoleService
{
    class Program
    {
        static void Main(string[] args)
        {
            ITaskManager manager = new TaskManager();
            manager.Run();
            Console.ReadKey();
            manager.Stop();
            Console.WriteLine("Press key...");
            Console.ReadKey();
        }
    }
}
