using Azure.Identity;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private ICommonService<ProductDto, ProductInserDto, ProductUpdateDto> _productService;
        private IReportService<ProductSellingDto> _reportService;

        public ProductController([FromKeyedServices("productService")] ICommonService<ProductDto, ProductInserDto, ProductUpdateDto> productService,
            [FromKeyedServices("productReportService")] IReportService<ProductSellingDto> reportService)
        {
            _productService = productService;
            _reportService = reportService;
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

        [HttpGet("bestselling")]
        public async Task<IEnumerable<ProductSellingDto>> GetBest() =>
            await _reportService.GetBest();

        [HttpGet("worstselling")]
        public async Task<IEnumerable<ProductSellingDto>> GetWors() =>
            await _reportService.GetWorst();

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Add(ProductInserDto insertDto)
        {
            var productDto = await _productService.Add(insertDto);

            return CreatedAtAction(nameof(GetById), new {id = productDto.Id}, productDto);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")] 
        public async Task<ActionResult<ProductDto>> Update(int id,  ProductUpdateDto updateDto)
        {
            var productDto = await _productService.Update(id, updateDto);

            return productDto == null ? NotFound() : Ok(productDto);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductDto>> Delete(int id)
        {
            var productDto = await _productService.Delete(id);

            return productDto == null ? NotFound() : Ok(productDto);
        }

        
    }
}
