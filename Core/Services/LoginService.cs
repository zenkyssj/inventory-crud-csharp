using Core.Models;
using Core.Models.Login;
using Core.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LoginService : ILoginService
    {
        private SistemaVentasContext _context;

        public LoginService(SistemaVentasContext context)
        {
            _context = context;
        }

        public AuthResponse Auth(AuthRequest request)
        {
            var response = new AuthResponse();

            string sPassword = Encrypt.GetSHA256(request.Password);

            var user = _context.Users.Where
                (d => d.Email == request.Email && d.Password == sPassword).FirstOrDefault();

            if (user == null) return null;

            response.Email = user.Email;

            return response;
        }
    }
}
