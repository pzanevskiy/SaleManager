using SaleManager.BL.DTO;
using SaleManager.BL.Service.Interfaces;
using SaleManager.DAL.UnitOfWork.Interfaces;
using SaleManager.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Service
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork Database { get; set; }
       
        public OrderService(IUnitOfWork uow)
        {           
            Database = uow;
        }
        
        public void AddOrder(OrderDTO orderDTO)
        {
            var cutomer = Database.Customers.Get(x => x.Nickname.Equals(orderDTO.Customer));
            var product = Database.Products.Get(x => x.Name.Equals(orderDTO.Product));

            Order order = new Order()
            {
                Date = orderDTO.Date,
                Price = orderDTO.Price
            };

            if (cutomer != null)
            {
                order.Customer = cutomer;
            }
            else
            {
                order.Customer = new Customer() { Nickname = orderDTO.Customer };
            }

            if (product != null)
            {
                order.Product = product;
            }
            else
            {
                order.Product = new Product() { Name = orderDTO.Product };
            }
            Database.Orders.Add(order);
            Database.Save();
        }

        public void Dispose()
        {
            Database = null;
            GC.SuppressFinalize(this);
        }

    }
}
