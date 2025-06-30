using Core.DTOs;
using Core.Models;
using Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class VentaService : ICommonService<VentaDto, VentaInsertDto, VentaUpdateDto>
    {
        private ICommonRepository<Ventum> _saleRepository;
        private IEditableRepository<Ventum> _editableRepository;
        private ITransactionRepository _transactionRepository;

        public VentaService(ICommonRepository<Ventum> saleRepository,
            IEditableRepository<Ventum> editableRepository,
            ITransactionRepository transactionRepository)
        {
            _saleRepository = saleRepository;
            _editableRepository = editableRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<VentaDto>> Get()
        {
            var ventas = await _saleRepository.Get();

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
            var venta = await _saleRepository.GetById(id);

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
            using (var transaction = _transactionRepository.Transaction())
            {
                try
                {
                    var venta = new Ventum()
                    {
                        Fecha = DateTime.Now,
                        Total = insertDto.Concepts.Sum(t => t.Cantidad * t.PrecioUnitario),
                        IdCliente = insertDto.IdCliente,

                    };

                    await _editableRepository.Add(venta);
                    await _editableRepository.Save();

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

                    await _editableRepository.Save();

                    var ventaDto = new VentaDto
                    {
                        Id = venta.Id,
                        Fecha = venta.Fecha,
                        Total = venta.Total,
                        IdCliente = venta.IdCliente
                    };

                    transaction.Commit();
                    return ventaDto;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return null;
        }

        public async Task<VentaDto> Update(int id, VentaUpdateDto updateDto)
        {
            var venta = await _saleRepository.GetById(id);

            if (venta != null)
            {
                venta.IdCliente = updateDto.IdCliente;

                _editableRepository.Update(venta);

                await _editableRepository.Save();

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
