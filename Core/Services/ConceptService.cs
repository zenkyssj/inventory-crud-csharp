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
    public class ConceptService : IConceptService<ConceptDto>, IReportService<ProductDto>
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

        public async Task<IEnumerable<ProductDto>> GetBest()
        {
            var concepts = await _context.Conceptos.ToListAsync();
            var products = await _context.Productos.ToListAsync();

            var bestSales = concepts.OrderBy(x => x.IdProducto)
                .GroupBy(c => c.IdProducto)
                .Select(p => new 
                {
                    ProductID = p.Key,
                    Count = p.Count(),
                }).ToList();

            var bestSellingProducts = products.Join(bestSales, p => p.Id, bs => bs.ProductID, (p, bs) => new { ProductID = p, Count = bs.Count })
                .OrderBy(x => x.Count)
                .Select(x => new ProductDto
                {
                    Id = x.ProductID.Id,
                    Nombre = x.ProductID.Nombre,
                    PrecioUnitario = x.ProductID.PrecioUnitario,
                    Costo = x.ProductID.Costo,
                    Id_Categoria = x.ProductID.IdCategoria
                }).ToList();

            return bestSellingProducts;
            
        }

        public Task<IEnumerable<ProductDto>> GetWorst()
        {
            throw new NotImplementedException();
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
