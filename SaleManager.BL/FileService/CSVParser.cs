using CsvHelper;
using SaleManager.BL.DTO;
using SaleManager.BL.FileService.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SaleManager.BL.FileService
{
    public class CSVParser : Interfaces.IParser
    {
        public IEnumerable<OrderDTO> Parse(string filename)
        {
            ICollection<OrderDTO> orders = new List<OrderDTO>();
            using(var stream=new StreamReader(filename))
            {
                using (var csv = new CsvReader(stream, System.Globalization.CultureInfo.CurrentCulture))
                {
                    foreach (var item in csv.GetRecords<OrderDTO>())
                    {
                        orders.Add(item);
                    }
                }
            }
            return orders;
        }        
    }
}
