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
    public class ProductService : IProductService
    {
        private IUnitOfWork Database { get; set; }
        IMapper mapper;

        public ProductService(IUnitOfWork uow)
        {
            Database = uow;
            mapper = new Mapper(MapperConfig.Configure());
        }

        public IEnumerable<ProductDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(Database.Products.Get());
        }

        public ProductDTO FindById(int id)
        {
            var product = mapper.Map<Product, ProductDTO>(Database.Products.Get(x => x.Id.Equals(id)));
            return product;
        }

        public void Create(ProductDTO productDTO)
        {
            var product = mapper.Map<ProductDTO, Product>(productDTO);
            Database.Products.Add(product);
            Database.Save();
        }

        public void Delete(int id)
        {
            var product = Database.Products.Get(x => x.Id.Equals(id));
            Database.Products.Delete(product);
            Database.Save();
        }

        public void Update(ProductDTO productDTO)
        {
            var product = mapper.Map<ProductDTO, Product>(productDTO);
            Database.Products.Update(product);
            Database.Save();
        }
    }
}
