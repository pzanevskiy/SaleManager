using AutoMapper;
using SaleManager.BL.DTO;
using SaleManager.BL.Service.Interfaces;
using SaleManager.BL.Util;
using SaleManager.DAL.UnitOfWork.Interfaces;
using SaleManager.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Service
{
    public class CustomerService : ICustomerService
    {
        private IUnitOfWork Database { get; set; }
        IMapper mapper;

        public CustomerService(IUnitOfWork uow)
        {
            Database = uow;
            mapper = new Mapper(MapperConfig.Configure());
        }

        public IEnumerable<CustomerDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDTO>>(Database.Customers.Get());
        }

        public CustomerDTO FindById(int id)
        {
            var customer = mapper.Map<Customer, CustomerDTO>(Database.Customers.Get(x => x.Id.Equals(id)));
            return customer;
        }

        public void Create(CustomerDTO customerDTO)
        {
            var customer = mapper.Map<CustomerDTO, Customer>(customerDTO);
            Database.Customers.Add(customer);
            Database.Save();
        }

        public void Delete(int id)
        {
            var customer = Database.Customers.Get(x => x.Id.Equals(id));
            Database.Customers.Delete(customer);
            Database.Save();
        }

        public void Update(CustomerDTO customerDTO)
        {
            var customer = mapper.Map<CustomerDTO, Customer>(customerDTO);
            Database.Customers.Update(customer);
            Database.Save();
        }
    }
}
