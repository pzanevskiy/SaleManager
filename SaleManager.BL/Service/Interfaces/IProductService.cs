using SaleManager.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.Service.Interfaces
{
    public interface IProductService
    {
        IEnumerable<ProductDTO> GetAll();
        ProductDTO FindById(int id);
        void Create(ProductDTO productDTO);
        void Delete(int id);
        void Update(ProductDTO productDTO);
    }
}
