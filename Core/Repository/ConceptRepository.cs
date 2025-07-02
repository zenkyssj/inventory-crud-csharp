using Core.DTOs;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repository
{
    public class ConceptRepository : ICommonRepository<Concepto>, IEditableRepository<Concepto>, IHelperRepository<Ventum>
    {
        private SistemaVentasContext _context;

        public ConceptRepository(SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Concepto>> Get()
            => await _context.Conceptos.ToListAsync();

        public async Task<Concepto> GetById(int id)
            => await _context.Conceptos.FindAsync(id);

        public async Task Add(Concepto concept)
            => await _context.Conceptos.AddAsync(concept);

        public void Update(Concepto concept)
        {
            _context.Attach(concept);
            _context.Entry(concept).State = EntityState.Modified;

        }

        public void Delete(Concepto concept)
            => _context.Conceptos.Remove(concept);

        public async Task Save()
            => await _context.SaveChangesAsync();


        public void DeleteAll(Ventum sale)
        {
            var conceptos = _context.Conceptos.Where(c => c.IdVenta == sale.Id);
            _context.Conceptos.RemoveRange(conceptos);
        }
    }
}
