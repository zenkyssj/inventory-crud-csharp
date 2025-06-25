using Azure.Identity;
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
    public class ConceptController : ControllerBase
    {
        private IConceptService<ConceptDto> _conceptService;
        private IReportService<ProductDto> _conceptReportService;

        public ConceptController([FromKeyedServices("conceptService")] IConceptService<ConceptDto> conceptService,
            [FromKeyedServices("conceptReportService")] IReportService<ProductDto> conceptReportService)
        {
            _conceptService = conceptService;
            _conceptReportService = conceptReportService;
        }

        [HttpGet]
        public async Task<IEnumerable<ConceptDto>> Get()
            => await _conceptService.Get();


        [HttpGet("{id}")]
        public async Task<ActionResult<ConceptDto>> GetById(int id)
        {
            var conceptDto = await _conceptService.GetById(id);

            return conceptDto == null ? NotFound() : Ok(conceptDto);
        }

    }
}
