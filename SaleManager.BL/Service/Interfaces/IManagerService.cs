using SaleManager.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Service.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<ManagerDTO> GetAll();
        ManagerDTO FindById(int id);
        void Create(ManagerDTO managerDTO);
        void Delete(int id);
        void Update(ManagerDTO managerDTO);
    }
}
