using Core.Models;
using Microsoft.EntityFrameworkCore;
using Core.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : ICommonRepository<User>, IEditableRepository<User>
    {
        private SistemaVentasContext _context;

        public UserRepository(SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> Get()
            => await _context.Users.ToListAsync();


        public async Task<User> GetById(int id)
            => await _context.Users.FindAsync(id);
        public async Task Add(User user)
            => await _context.Users.AddAsync(user);

        public void Update(User user)
        {
            _context.Users.Attach(user);
            _context.Users.Entry(user).State = EntityState.Modified;
        }

        public void Delete(User user)
            => _context.Users.Remove(user);

        public async Task Save()
            => await _context.SaveChangesAsync();

 
    }
}
