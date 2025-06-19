using Core.Models;
using Core.Models.Common;
using Core.Models.Login;
using Core.Tools;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class LoginService : ILoginService
    {
        private SistemaVentasContext _context;
        private readonly AppSettings _settings;

        public LoginService(SistemaVentasContext context, IOptions<AppSettings> settings)
        {
            _context = context;
            _settings = settings.Value;
        }

        public AuthResponse Auth(AuthRequest request)
        {
            var response = new AuthResponse();

            string sPassword = Encrypt.GetSHA256(request.Password);

            var user = _context.Users.Where
                (d => d.Email == request.Email && d.Password == sPassword).FirstOrDefault();

            if (user == null) return null;

            response.Email = user.Email;
            response.Token = GetToken(user);

            return response;
        }

        private string GetToken(User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_settings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new(ClaimTypes.Email, user.Email),
                        new(ClaimTypes.Role, user.Rol.ToString())
                    ]
                    ),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = jwtHandler.CreateToken(tokenDescriptor);

            return jwtHandler.WriteToken(token);
        }
    }
}
