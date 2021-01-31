using SaleManager.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Service.Interfaces
{
    public interface IOrderService : IDisposable
    {
        void AddOrder(OrderDTO orderDTO);
    }
}
