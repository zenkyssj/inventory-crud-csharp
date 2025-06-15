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
    public class CategoryService : ICommonService<CategoryDto, CategoryInsertDto, CategoryUpdateDto>
    {
        private SistemaVentasContext _context;

        public CategoryService (SistemaVentasContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CategoryDto>> Get()
        {
            var categorys = await _context.Categoria.ToListAsync();

            return categorys.Select(c => new CategoryDto
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion
            });
        }

        public async Task<CategoryDto> GetById(int id)
        {
            var category = await _context.Categoria.FindAsync(id);

            if (category != null)
            {
                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Nombre = category.Nombre,
                    Descripcion = category.Descripcion
                };

                return categoryDto;
            }

            return null;
        }

        public async Task<CategoryDto> Add(CategoryInsertDto insertDto)
        {
            var category = new Categorium()
            {
                Nombre = insertDto.Nombre,
                Descripcion = insertDto.Descripcion,
            };

            await _context.Categoria.AddAsync(category);
            await _context.SaveChangesAsync();

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Nombre = category.Nombre,
                Descripcion = category.Descripcion
            };

            return categoryDto;
        }

        public async Task<CategoryDto> Update(int id, CategoryUpdateDto updateDto)
        {
            var category = await _context.Categoria.FindAsync(id);

            if (category != null)
            {
                category.Nombre = updateDto.Nombre;
                category.Descripcion = updateDto.Descripcion;

                _context.Categoria.Attach(category);
                _context.Categoria.Entry(category).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Nombre = category.Nombre,
                    Descripcion = category.Descripcion
                };

                return categoryDto;
            }

            return null;
        }

        public async Task<CategoryDto> Delete(int id)
        {
            var category = await _context.Categoria.FindAsync(id);

            if (category != null)
            {
                var categoryDto = new CategoryDto
                {
                    Id = category.Id,
                    Nombre = category.Nombre,
                    Descripcion = category.Descripcion
                };

                _context.Remove(category);
                await _context.SaveChangesAsync();

                return categoryDto;
            }

            return null;
        }
    }
}
