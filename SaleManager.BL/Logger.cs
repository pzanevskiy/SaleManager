using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text;

namespace SaleManager.BL
{
    public class Logger
    {
        private string filePath;
        private static StreamWriter writer;
        private static object locker = new object();

        public Logger()
        {
            this.filePath = ConfigurationManager.AppSettings["log"];
        }

        public void Info(string message)
        {
            lock (locker)
            {
                try
                {
                    if (writer == null)
                    {
                        writer = new StreamWriter(filePath,true);
                    }
                    else
                    {
                        writer = File.AppendText(filePath);
                    }
                    writer.WriteLine($"{DateTime.Now} - {message}");
                }
                catch (IOException)
                {
                    throw new InvalidOperationException("cannot log message");
                }
                finally
                {
                    writer.Close();
                    writer.Dispose();
                }
            }
        }
    }
}
