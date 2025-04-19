using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GestordeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NinosController : ControllerBase
    {
        private readonly INinoService _ninoService;

        public NinosController(INinoService ninoService)
        {
            _ninoService = ninoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWithTutorAndGuarderia()
        {
            var ninos = await _ninoService.GetAllWithTutorAndGuarderiaAsync();
            return Ok(ninos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var nino = await _ninoService.GetByIdWithTutorAndGuarderiaAsync(id);
                return Ok(nino);
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Nino no encontrado" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NinoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _ninoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NinoDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var actualizado = await _ninoService.UpdateAsync(id, dto);
            if (!actualizado)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var eliminado = await _ninoService.DeleteAsync(id);
            if (!eliminado)
                return NotFound();

            return NoContent();
        }

    }
}
