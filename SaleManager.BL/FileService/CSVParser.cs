using CsvHelper;
using SaleManager.BL.DTO;
using SaleManager.BL.FileService.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace SaleManager.BL.FileService
{
    public class CSVParser : Interfaces.IParser
    {
        private Logger _logger;

        public CSVParser()
        {
            _logger = new Logger();
        }

        public IEnumerable<OrderDTO> Parse(string filename)
        {
            ICollection<OrderDTO> orders = new List<OrderDTO>();
            try
            {
                using (var stream = new StreamReader(filename))
                {
                    using (var csv = new CsvReader(stream, null))
                    {
                        foreach (var item in csv.GetRecords<OrderDTO>())
                        {
                            orders.Add(item);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                _logger.Info(e.Message);
                throw new Exception(e.Message);
            }
            return orders;
        }

        public IEnumerable<OrderDTO> ManualParse(string filename)
        {
            ICollection<OrderDTO> orders = new List<OrderDTO>();
            try
            {
                using (var stream = new StreamReader(filename))
                {
                    stream.ReadLine();
                    while (!stream.EndOfStream)
                    {
                        string[] splitted = stream.ReadLine().Split(';');

                        OrderDTO order = new OrderDTO()
                        {
                            Date = DateTime.ParseExact(splitted[0], "dd.MM.yyyy", null),
                            Customer = splitted[1],
                            Product = splitted[2],
                            Price = double.Parse(splitted[3].Trim(new char[] { '\"' }), null)
                        };

                        orders.Add(order);
                    }
                    stream.Close();
                }
            }
            catch(IOException e)
            {
                _logger.Info(e.Message);
                throw new Exception(e.Message);
            }
            
            return orders;
        }
    }
}
