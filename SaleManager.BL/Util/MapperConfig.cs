using AutoMapper;
using SaleManager.BL.DTO;
using SaleManager.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Util
{
    public class MapperConfig
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.CreateMap<Product, ProductDTO>();
                    cfg.CreateMap<ProductDTO, Product>();
                    cfg.CreateMap<Customer, CustomerDTO>();
                    cfg.CreateMap<CustomerDTO, Customer>();
                    cfg.CreateMap<Manager, ManagerDTO>();
                    cfg.CreateMap<ManagerDTO, Manager>();
                    cfg.CreateMap<Order, OrderDTO>()
                    .ForMember("Customer", opt => opt.MapFrom(x => x.Customer.Nickname))
                    .ForMember("Product", opt => opt.MapFrom(x => x.Product.Name))
                    .ForMember("Manager", opt => opt.MapFrom(x => x.Manager.LastName));
                    cfg.CreateMap<OrderDTO, Order>();
                }
            );
            return config;
        }
    }
}
