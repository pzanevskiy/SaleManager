using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SaleManager.BL
{
    public class Logger 
    {
        string filePath;
        private bool _disposed = false;
        private StreamWriter writer;
        public Logger()
        {
           
            writer = new StreamWriter("D:\\log.txt", false);
        }
        public void LogInfo(string message)
        {
            lock (writer)
            {
                try
                {
                    writer.WriteLine("Date: " + DateTime.Now.ToString() + " Log info: " + message);
                }
                catch (IOException e)
                {
                    throw new InvalidOperationException("cannot log message", e);
                }
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }
            if (disposing)
            {
                writer.Close();
                writer.Dispose();
            }
            _disposed = true;
        }
        ~Logger()
        {
            Dispose();
        }
    }
}
