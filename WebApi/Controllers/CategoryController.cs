using Core.DTOs;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private ICommonService<CategoryDto, CategoryInsertDto, CategoryUpdateDto> _categoryService;

        public CategoryController([FromKeyedServices("categoryService")]ICommonService<CategoryDto, CategoryInsertDto, CategoryUpdateDto> categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Get()
            => await _categoryService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var categoryDto = await _categoryService.GetById(id);

            return categoryDto == null ? NotFound() : Ok(categoryDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Add(CategoryInsertDto inserDto)
        {
            var categoryDto = await _categoryService.Add(inserDto);

            return CreatedAtAction(nameof(GetById), new { id = categoryDto.Id }, categoryDto);        
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, CategoryUpdateDto inserDto)
        {
            var categoryDto= await _categoryService.Update(id, inserDto);

            return categoryDto == null ? NotFound() : Ok(categoryDto);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoryDto>> Delete(int id)
        {
            var categoryDto = await _categoryService.Delete(id);

            return categoryDto == null ? NotFound() : Ok(categoryDto);
        }

    }
}
