using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.TaskManager.Interfaces
{
    public interface ITaskManager : IDisposable
    {
        void Run();
        void Stop();
    }
}
