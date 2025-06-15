using Azure.Identity;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ICommonService<ProductDto, ProductInserDto, ProductUpdateDto> _productService;

        public ProductController([FromKeyedServices("productService")] ICommonService<ProductDto, ProductInserDto, ProductUpdateDto> productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductDto>> Get() =>
            await _productService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var productDto = await _productService.GetById(id);

            return productDto == null ? NotFound() : Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> Add(ProductInserDto insertDto)
        {
            var productDto = await _productService.Add(insertDto);

            return CreatedAtAction(nameof(GetById), new {id = productDto.Id}, productDto);
        }

        [HttpPut("{id}")] 
        public async Task<ActionResult<ProductDto>> Update(int id,  ProductUpdateDto updateDto)
        {
            var productDto = await _productService.Update(id, updateDto);

            return productDto == null ? NotFound() : Ok(productDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            var productDto = await _productService.Delete(id);

            return productDto == null ? NotFound() : Ok(productDto);
        }

        
    }
}
