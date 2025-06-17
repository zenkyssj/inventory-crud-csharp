using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private ICommonService<VentaDto, VentaInsertDto, VentaUpdateDto> _ventaService;

        public VentaController([FromKeyedServices("ventaService")] ICommonService<VentaDto, VentaInsertDto, VentaUpdateDto> ventaService)
        {
            _ventaService = ventaService;
        }

        [HttpGet]
        public async Task<IEnumerable<VentaDto>> Get()
            => await _ventaService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<VentaDto>> GetById(int id)
        {
            var ventaDto = await _ventaService.GetById(id);

            return ventaDto == null ? NotFound() : Ok(ventaDto);
        }

        [HttpPost]
        public async Task<ActionResult<VentaDto>> Add(VentaInsertDto insertDto)
        {
            var ventaDto = await _ventaService.Add(insertDto);

            return ventaDto == null ? BadRequest() : CreatedAtAction(nameof(GetById), new { id = ventaDto.Id }, ventaDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VentaDto>> Update(int id, VentaUpdateDto updateDto)
        {
            var ventaDto = await _ventaService.Update(id, updateDto);

            return ventaDto == null ? NotFound() : Ok(ventaDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VentaDto>> Delete(int id)
        {
            var ventaDto = await _ventaService.Delete(id);

            return ventaDto == null? NotFound() : Ok(ventaDto);
        }        
    }
}
