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
            //IParser parser = new CSVParser();
            //var x=parser.Parse("C:\\Users\\Павел\\source\\repos\\SaleManager\\Files\\2.csv");
            //using(var uow=new EFUnitOfWork())
            //{
            //    OrderService orderService = new OrderService(uow);
            //    foreach(var item in x)
            //    {
            //        orderService.AddOrder(item);
            //    }
            //    var cust = uow.Customers.Get();
                
            //}
        }
    }
}
