using Core.DTOs;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class ProductService : ICommonService<ProductDto, ProductInserDto, ProductUpdateDto> 
    {
        private SistemaVentasContext _context;

        public ProductService(SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> Get()
        {
            var products = await _context.Productos.ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                PrecioUnitario = p.PrecioUnitario,
                Costo = p.Costo,
                Id_Categoria = p.IdCategoria
            });
        }

        public async Task<ProductDto> GetById(int id)
        {
            var product = await _context.Productos.FindAsync(id);

            if (product != null)
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Nombre = product.Nombre,
                    PrecioUnitario = product.PrecioUnitario,
                    Costo = product.Costo,
                    Id_Categoria = product.IdCategoria
                };

                return productDto;
            }

            return null;
        }


        public async Task<ProductDto> Add(ProductInserDto insertDto)
        {
            var product = new Producto()
            {
                Nombre = insertDto.Nombre,
                PrecioUnitario = insertDto.PrecioUnitario,
                Costo = insertDto.Costo,
                IdCategoria = insertDto.Id_Categoria
            };

            await _context.AddAsync(product);
            await _context.SaveChangesAsync();

            var productDto = new ProductDto
            {
                Id = product.Id,
                Nombre = product.Nombre,
                PrecioUnitario = product.PrecioUnitario,
                Costo = product.Costo,
                Id_Categoria = product.IdCategoria
            };

            return productDto;
        }

        public async Task<ProductDto> Update(int id, ProductUpdateDto updateDto)
        {
            var product = await _context.Productos.FindAsync(id);

            if (product != null)
            {
                product.Nombre = updateDto.Nombre;
                product.PrecioUnitario = updateDto.PrecioUnitario;
                product.Costo = updateDto.Costo;
                product.IdCategoria = updateDto.Id_Categoria;

                _context.Productos.Attach(product);
                _context.Productos.Entry(product).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Nombre = product.Nombre,
                    PrecioUnitario = product.PrecioUnitario,
                    Costo = product.Costo,
                    Id_Categoria = product.IdCategoria
                };

                return productDto;
            }

            return null;
        }

        public async Task<ProductDto> Delete(int id)
        {
            var product = await _context.Productos.FindAsync(id);

            if (product != null)
            {
                var productDto = new ProductDto
                {
                    Id = product.Id,
                    Nombre = product.Nombre,
                    PrecioUnitario = product.PrecioUnitario,
                    Costo = product.Costo,
                    Id_Categoria = product.IdCategoria
                };

                _context.Productos.Remove(product);
                await _context.SaveChangesAsync();

                return productDto;
            }

            return null;
        }


    }
}
