using SaleManager.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Service.Interfaces
{
    public interface IOrderService : IDisposable
    {
        void AddOrder(OrderDTO orderDTO);
        IEnumerable<OrderDTO> GetAll();
        OrderDTO FindById(int id);
        void Delete(int id);
        void Update(OrderDTO orderDTO);
        IEnumerable<OrderDTO> GetOrdersByCustomerId(int id);
        IEnumerable<OrderDTO> GetOrdersByManagerId(int id);
    }
}
