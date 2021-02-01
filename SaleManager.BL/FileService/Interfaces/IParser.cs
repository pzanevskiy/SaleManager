using SaleManager.BL.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SaleManager.BL.FileService.Interfaces
{
    public interface IParser
    {
        IEnumerable<OrderDTO> Parse(string filename);
        IEnumerable<OrderDTO> HandleParse(string filename);
    }
}
