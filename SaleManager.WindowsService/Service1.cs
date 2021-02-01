using SaleManager.BL;
using SaleManager.BL.TaskManager;
using SaleManager.BL.TaskManager.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SaleManager.WindowsService
{
    public partial class Service1 : ServiceBase
    {
        private ITaskManager manager;
        CustomTaskScheduler scheduler;
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            manager = new TaskManager();
            manager.Run();
        }

        protected override void OnStop()
        {                      
            manager.Stop();
            manager.Dispose();
        }
    }
    
}
