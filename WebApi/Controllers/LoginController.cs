using Core.Models.Login;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;

        public LoginController([FromKeyedServices("loginService")] ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Auth([FromBody] AuthRequest request)
        {
            var user = _loginService.Auth(request);

            return user == null ? BadRequest() : Ok(user);
        }

    }
}
