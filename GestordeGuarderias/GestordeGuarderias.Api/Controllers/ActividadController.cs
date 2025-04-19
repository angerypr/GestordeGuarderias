using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace GestordeGuarderias.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActividadController : ControllerBase
    {
        private readonly IActividadService _actividadService;

        public ActividadController(IActividadService actividadService)
        {
            _actividadService = actividadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var actividades = await _actividadService.GetAllWithGuarderiaAsync();
                return Ok(actividades);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var actividad = await _actividadService.GetByIdAsync(id);
                return Ok(actividad);  
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });  
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ActividadDTO actividadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("El modelo no es válido");
            }

            try
            {
                var createdActividad = await _actividadService.CreateAsync(actividadDto);
                return CreatedAtAction(nameof(GetById), new { id = createdActividad.Id }, createdActividad);  
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] ActividadDTO actividadDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("El modelo no es válido");
            }

            try
            {
                await _actividadService.UpdateAsync(id, actividadDto);
                return NoContent();  
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deleted = await _actividadService.DeleteAsync(id);
                if (deleted)
                {
                    return NoContent();  
                }
                else
                {
                    return NotFound(new { message = "Actividad no encontrada" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
