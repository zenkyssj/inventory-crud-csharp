using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IUserService<Dto, InsertDto, UpdateDto>
    {
        Task<IEnumerable<Dto>> Get();
        Task<Dto> GetById(Dto dto);
        Task<Dto> Add(InsertDto insertDto); 
        Task<Dto> Update(int id, UpdateDto updateDto);
        Task<Dto> Delete(int id);   
    }
}
