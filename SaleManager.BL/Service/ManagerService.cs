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
    public class ManagerService : IManagerService
    {
        private IUnitOfWork Database { get; set; }
        IMapper mapper;

        public ManagerService(IUnitOfWork uow)
        {
            Database = uow;
            mapper = new Mapper(MapperConfig.Configure());
        }

        public IEnumerable<ManagerDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Manager>, IEnumerable<ManagerDTO>>(Database.Managers.Get());
        }

        public ManagerDTO FindById(int id)
        {
            return mapper.Map<Manager, ManagerDTO>(Database.Managers.Get(x => x.Id.Equals(id)));
        }

        public void Create(ManagerDTO managerDTO)
        {
            var manager = mapper.Map<ManagerDTO, Manager>(managerDTO);
            Database.Managers.Add(manager);
            Database.Save();
        }

        public void Delete(int id)
        {
            var manager = Database.Managers.Get(x => x.Id.Equals(id));
            Database.Managers.Delete(manager);
            Database.Save();
        }

        public void Update(ManagerDTO managerDTO)
        {
            var manager = mapper.Map<ManagerDTO, Manager>(managerDTO);
            Database.Managers.Update(manager);
            Database.Save();
        }
    }
}
