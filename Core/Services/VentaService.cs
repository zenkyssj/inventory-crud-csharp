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
    public class VentaService : ICommonService<VentaDto, VentaInsertDto, VentaUpdateDto>
    {
        private SistemaVentasContext _context;

        public VentaService(SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<VentaDto>> Get()
        {
            var ventas = await _context.Venta.ToListAsync();

            return ventas.Select(v => new VentaDto
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total,
                IdCliente = v.IdCliente,
            });
        }

        public async Task<VentaDto> GetById(int id)
        {
            var venta = await _context.Venta.FindAsync(id);

            if (venta != null)
            {
                var ventaDto = new VentaDto
                {
                    Id = venta.Id,
                    Fecha = venta.Fecha,
                    Total = venta.Total,
                    IdCliente = venta.IdCliente
                };

                return ventaDto;
            }

            return null;
        }

        public async Task<VentaDto> Add(VentaInsertDto insertDto)
        {
            var venta = new Ventum()
            {
                Fecha = DateTime.Now,
                Total = insertDto.Total,
                IdCliente = insertDto.IdCliente,

            };

            await _context.AddAsync(venta);
            await _context.SaveChangesAsync();

            foreach (var concept in insertDto.Concepts)
            {
                var concepto = new Concepto()
                {
                    IdVenta = venta.Id,
                    Cantidad = concept.Cantidad,
                    PrecioUnitario = concept.PrecioUnitario,
                    Importe = concept.Importe,
                    IdProducto = concept.IdProducto                  
                };
                await _context.AddAsync(concepto);
                
            }

            await _context.SaveChangesAsync();

            var ventaDto = new VentaDto
            {
                Id = venta.Id,
                Fecha = venta.Fecha,
                Total = venta.Total,
                IdCliente = venta.IdCliente
            };

            return ventaDto;
        }

        public async Task<VentaDto> Update(int id, VentaUpdateDto updateDto)
        {
            var venta = await _context.Venta.FindAsync(id);

            if (venta != null)
            {
                venta.IdCliente = updateDto.IdCliente;

                _context.Venta.Attach(venta);
                _context.Venta.Entry(venta).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var ventaDto = new VentaDto
                {
                    Id = venta.Id,
                    Fecha = venta.Fecha,
                    Total = venta.Total,
                    IdCliente = venta.IdCliente
                };

                return ventaDto;
            }

            return null;
        }

        public async Task<VentaDto> Delete(int id)
        {
            var venta = await _context.Venta.FindAsync(id);

            if (venta != null)
            {
                var conceptos = _context.Conceptos.Where(c => c.IdVenta == venta.Id);
                _context.Conceptos.RemoveRange(conceptos); // Elimina primero los conceptos asociados a la venta.

                var ventaDto = new VentaDto
                {
                    Id = venta.Id,
                    Fecha = venta.Fecha,
                    Total = venta.Total,
                    IdCliente = venta.IdCliente
                };

                _context.Venta.Remove(venta);
                await _context.SaveChangesAsync();

                return ventaDto;
            }

            return null;
        }
    }
}
