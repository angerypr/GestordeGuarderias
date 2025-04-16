using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GestordeGuarderias.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TutoresController : ControllerBase
    {
        private readonly ITutorService _tutorService;

        public TutoresController(ITutorService tutorService)
        {
            _tutorService = tutorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tutores = await _tutorService.GetAllAsync();
            return Ok(tutores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var tutor = await _tutorService.GetByIdAsync(id);
                return Ok(tutor);
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Tutor no encontrado" });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TutorDTO tutorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("El modelo es inválido");
            }

            var tutor = await _tutorService.CreateAsync(tutorDto);
            return Ok(new { success = true, id = tutor.Id });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TutorDTO tutorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("El modelo es inválido");
            }

            try
            {
                await _tutorService.UpdateAsync(id, tutorDto);
                return Ok(new { success = true });
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Tutor no encontrado" });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _tutorService.DeleteAsync(id);
                return Ok(new { success = true });
            }
            catch (Exception)
            {
                return NotFound(new { success = false, message = "Tutor no encontrado" });
            }
        }

    }
}
