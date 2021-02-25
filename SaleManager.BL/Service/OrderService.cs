using AutoMapper;
using SaleManager.BL.DTO;
using SaleManager.BL.Service.Interfaces;
using SaleManager.BL.Util;
using SaleManager.DAL.UnitOfWork.Interfaces;
using SaleManager.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SaleManager.BL.Service
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork Database { get; set; }
        IMapper mapper;
        public OrderService(IUnitOfWork uow)
        {           
            Database = uow;
            mapper = new Mapper(MapperConfig.Configure());
        }

        public OrderDTO FindById(int id)
        {
            var order = mapper.Map<Order, OrderDTO>(Database.Orders.Get(x => x.Id.Equals(id)));
            return order;
        }

        public void Update(OrderDTO orderDTO)
        {
            var order = Database.Orders.Get(x=>x.Id.Equals(orderDTO.Id));
            order.Date = orderDTO.Date;
            order.Price = orderDTO.Price;
            order.Customer = Database.Customers.Get(x=>x.Nickname.Equals(orderDTO.Customer));
            order.Product = Database.Products.Get(x => x.Name.Equals(orderDTO.Product));
            order.Manager = Database.Managers.Get(x =>x.LastName.Equals(orderDTO.Manager));
            Database.Orders.Update(order);
            Database.Save();
        }

        public void Delete(int id)
        {
            var order = Database.Orders.Get(x => x.Id.Equals(id));
            Database.Orders.Delete(order);
            Database.Save();
        }

        public void AddOrder(OrderDTO orderDTO)
        {
            var cutomer = Database.Customers.Get(x => x.Nickname.Equals(orderDTO.Customer));
            var product = Database.Products.Get(x => x.Name.Equals(orderDTO.Product));
            var manager = Database.Managers.Get(x => x.LastName.Equals(orderDTO.Manager));
            var order = new Order()
            {
                Date = orderDTO.Date,
                Price = orderDTO.Price
            };

            if (cutomer != null)
            {
                order.Customer = cutomer;
            }            

            if (product != null)
            {
                order.Product = product;
            }            

            if (manager != null)
            {
                order.Manager = manager;
            }
            
            Database.Orders.Add(order);
            Database.Save();
        }

        public IEnumerable<OrderDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Database.Orders.Get());
        }

        public void Dispose()
        {
            Database = null;
            GC.SuppressFinalize(this);
        }

        public IEnumerable<OrderDTO> GetOrdersByCustomerId(int id)
        {
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Database.Orders.Get().Where(x=>x.Customer.Id.Equals(id)));            
        }

        public IEnumerable<OrderDTO> GetOrdersByManagerId(int id)
        {
            return mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(Database.Orders.Get().Where(x => x.Manager.Id.Equals(id)));
        }
    }
}
