using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestordeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AsistenciaController : ControllerBase
    {
        private readonly IAsistenciaService _asistenciaService;

        public AsistenciaController(IAsistenciaService asistenciaService)
        {
            _asistenciaService = asistenciaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var asistencias = await _asistenciaService.GetAllAsync();
            return Ok(asistencias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var asistencia = await _asistenciaService.GetByIdAsync(id);
            if (asistencia == null) return NotFound();
            return Ok(asistencia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AsistenciaDTO dto)
        {
            var nueva = await _asistenciaService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = nueva.Id }, nueva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AsistenciaDTO dto)
        {
            var updated = await _asistenciaService.UpdateAsync(id, dto);
            if (!updated) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _asistenciaService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
