using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class SaleRepository : ICommonRepository<Ventum>, IEditableRepository<Ventum>, ITransactionRepository
    {
        private SistemaVentasContext _context;

        public SaleRepository(SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ventum>> Get()
            => await _context.Venta.ToListAsync();

        public async Task<Ventum> GetById(int id)
            => await _context.Venta.FindAsync(id);

        public async Task Add(Ventum sale)
            => await _context.Venta.AddAsync(sale);

        public void Update(Ventum sale)
        {
            _context.Venta.Attach(sale);
            _context.Venta.Entry(sale).State = EntityState.Modified;
        }

        public void Delete(Ventum sale)
            => _context.Venta.Remove(sale);

        public IDbContextTransaction Transaction()
            => _context.Database.BeginTransaction();
        public async Task Save()
            => await _context.SaveChangesAsync();
    }
}
