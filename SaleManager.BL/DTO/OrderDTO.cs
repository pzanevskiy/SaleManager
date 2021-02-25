using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Manager { get; set; }
        public double Price { get; set; }
    }
}
