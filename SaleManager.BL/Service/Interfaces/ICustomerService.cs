using SaleManager.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Service.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<CustomerDTO> GetAll();
        CustomerDTO FindById(int id);
        void Create(CustomerDTO customerDTO);
        void Delete(int id);
        void Update(CustomerDTO customerDTO);
    }
}
