using SaleManager.BL.FileService.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SaleManager.BL.FileService
{
    public class DirectoryHandler : IDirectoryHandler
    {
        private Logger _logger;
        private string _destinationDirectory;

        public DirectoryHandler()
        {
            _logger = new Logger();
            _destinationDirectory = ConfigurationManager.AppSettings["destFolder"];
        }

        public void Move(string filePath, string fileName)
        {
            try
            {
                File.Move(filePath, _destinationDirectory + "\\" + fileName);
                _logger.Info($"{fileName} moved to destination directory");
            }
            catch (IOException e)
            {
                _logger.Info(e.Message);
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}
