using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductService : ICommonService<ProductDto, ProductInserDto, ProductUpdateDto>
    {
        public Task<ProductDto> Add(ProductInserDto insertDto)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> Get()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetById(ProductDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> Update(int id, ProductUpdateDto updateDto)
        {
            throw new NotImplementedException();
        }
    }
}
