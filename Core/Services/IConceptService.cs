using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IConceptService<TDto>
    {
        Task<IEnumerable<TDto>> Get();
        Task<TDto> GetById(int id);
    }
}
