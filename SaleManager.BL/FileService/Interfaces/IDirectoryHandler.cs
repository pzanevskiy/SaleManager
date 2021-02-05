using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.FileService.Interfaces
{
    public interface IDirectoryHandler
    {
        void Move(string filePath, string fileName);
    }
}
