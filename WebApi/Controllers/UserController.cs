using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Core.Models;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        [HttpGet]
        public IActionResult Get()
        {
            using (SistemaVentasContext db = new SistemaVentasContext())
            {
                var lst = db.Users.ToList();
                return Ok(lst);
            }         
        }
    }
}
