using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
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
        public async Task<IActionResult> GetAll()
        {
            var ninos = await _ninoService.GetAllAsync();
            return Ok(ninos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var nino = await _ninoService.GetByIdAsync(id);
                return Ok(nino);
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Nino no encontrado" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NinoDTO ninoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("El modelo es inválido");
            }

            var nino = await _ninoService.CreateAsync(ninoDto);
            return Ok(new { success = true, id = nino.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] NinoDTO ninoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("El modelo es inválido");
            }

            try
            {
                await _ninoService.UpdateAsync(id, ninoDto);
                return Ok(new { success = true });
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Nino no encontrado" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _ninoService.DeleteAsync(id);
                return Ok(new { success = true });
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Nino no encontrado" });
            }
        }

    }
}
