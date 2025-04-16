using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestordeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GuarderiasController : ControllerBase
    {
        private readonly IGuarderiaService _guarderiaService;

        public GuarderiasController(IGuarderiaService guarderiaService)
        {
            _guarderiaService = guarderiaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var guarderias = await _guarderiaService.GetAllAsync();
            return Ok(guarderias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var guarderia = await _guarderiaService.GetByIdAsync(id);
            if (guarderia == null)
                return NotFound(new { success = false, message = "Guardería no encontrada" });

            return Ok(guarderia);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GuarderiaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("El modelo es inválido");

            var result = await _guarderiaService.CreateAsync(dto);
            return Ok(new { success = true, id = result.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] GuarderiaDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("El modelo es inválido");

            try
            {
                await _guarderiaService.UpdateAsync(id, dto);
                return Ok(new { success = true });
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Guardería no encontrada" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _guarderiaService.DeleteAsync(id);
                return Ok(new { success = true });
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Guardería no encontrada" });
            }
        }
    }
}
