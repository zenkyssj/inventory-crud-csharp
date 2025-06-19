using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Tools
{
    public class TokenValidator
    {
        private SistemaVentasContext _context;

        public TokenValidator(SistemaVentasContext context)
        {
            _context = context;
        }

        public dynamic Validate(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    throw new Exception("No claims found in identity");
                }

                var idClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                if (idClaim == null || !int.TryParse(idClaim, out int id))
                {
                    throw new Exception("Invalid or missing user ID claim");
                }

                var user = _context.Users.FirstOrDefault(x => x.Id == id);
                if (user == null)
                {
                    throw new Exception("User not found");
                }

                return user;
            }
            catch (Exception ex)
            {
                return ex;
            }
        }
    }
}
