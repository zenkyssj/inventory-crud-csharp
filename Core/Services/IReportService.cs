using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IReportService<TDto>
    {
        Task<IEnumerable<TDto>> GetBest();
        Task<IEnumerable<TDto>> GetWorst();
    }
}
