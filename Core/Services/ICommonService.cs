using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ICommonService<TDto, TInsertDto, TUpdateDto>
    {
        Task<IEnumerable<TDto>> Get();
        Task<TDto> GetById(int id);
        Task<TDto> Add(TInsertDto insertDto); 
        Task<TDto> Update(int id, TUpdateDto updateDto);
        Task<TDto> Delete(int id);   
    }
}
