using Core.Models.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface ILoginService
    {
        AuthResponse Auth(AuthRequest request);
    }
}
