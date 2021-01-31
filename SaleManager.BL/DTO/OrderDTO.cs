using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.DTO
{
    public class OrderDTO
    {
        public DateTime Date { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return Date.ToString("d") + " " + Customer + " " + Product + " " + Price;
        }
    }
}
