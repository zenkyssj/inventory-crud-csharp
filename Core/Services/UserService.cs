using Core.DTOs;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserService : ICommonService<UserDto, UserInserDto, UserUpdateDto>
    {
        private SistemaVentasContext _context;

        public UserService(SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserDto>> Get()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
            });
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                };

                return userDto;
            }

            return null;
        }

        public async Task<UserDto> Add(UserInserDto insertDto)
        {
            var user = new User()
            {
                Name = insertDto.Name,
                LastName = insertDto.LastName,
                Email = insertDto.Email,
                Password = insertDto.Password,

            };

            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password,
            };

            return userDto;
        }

        public async Task<UserDto> Update(int id, UserUpdateDto updateDto)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                user.Name = updateDto.Name;
                user.LastName = updateDto.LastName;
                user.Email = updateDto.Email;
                user.Password = updateDto.Password;

                _context.Users.Attach(user);
                _context.Users.Entry(user).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = updateDto.Name,
                    LastName = updateDto.LastName,
                    Email = updateDto.Email,
                    Password = updateDto.Password,

                };

                return userDto;
            }
            return null;
        }

        public async Task<UserDto> Delete(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Name = user.Name,
                    LastName = user.LastName,
                    Email = user.Email,
                    Password = user.Password,
                };

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return userDto;
            }

            return null;
        }
    }
}
