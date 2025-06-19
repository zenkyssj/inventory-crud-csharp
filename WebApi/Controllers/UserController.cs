using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Core.Models;
using Core.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Authorization;
using Core.Tools;
using System.Security.Claims;


namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private ICommonService<UserDto, UserInserDto, UserUpdateDto> _userService;

        public UserController([FromKeyedServices("userService")] ICommonService<UserDto, UserInserDto, UserUpdateDto> userService) 
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDto>> Get() =>  
           await _userService.Get();       
        

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            var userDto = await _userService.GetById(id);

            return userDto == null ? NotFound(): Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Add(UserInserDto userInserDto)
        {
            var userDto = await _userService.Add(userInserDto);

            return Ok(userDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, UserUpdateDto userUpdate)
        {
            var userDto = await _userService.Update(id, userUpdate);

            return userDto == null ? NotFound() : Ok(userDto);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> Delete(int id)
        {

            var userDto = await _userService.Delete(id);

            return userDto == null? NotFound() : Ok(userDto);
        }
    }
}
