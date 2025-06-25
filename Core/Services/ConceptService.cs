using Core.DTOs;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ConceptService : IConceptService<ConceptDto>
    {
        private SistemaVentasContext _context;

        public ConceptService(SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConceptDto>> Get()
        {
            var concepts = await _context.Conceptos.ToListAsync();

            return concepts.Select(c => new ConceptDto
            {
                Id = c.Id,
                IdVenta = c.IdVenta,
                Cantidad = c.Cantidad,
                PrecioUnitario = c.PrecioUnitario,
                Importe = c.Importe,
                IdProducto = c.IdProducto,
            });
        }

        public async Task<ConceptDto> GetById(int id)
        {
            var concept = await _context.Conceptos.FindAsync(id);

            if (concept != null)
            {
                var conceptDto = new ConceptDto
                {
                    Id = concept.Id,
                    IdVenta = concept.IdVenta,
                    Cantidad = concept.Cantidad,
                    PrecioUnitario = concept.PrecioUnitario,
                    Importe = concept.Importe,
                    IdProducto = concept.IdProducto
                };

                return conceptDto;
            }

            return null;
        }


    }
}
