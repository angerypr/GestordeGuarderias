using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestordeGuarderias.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadNinoController : ControllerBase
    {
        private readonly IActividadNinoService _actividadNinoService;
        private readonly ILogger<ActividadNinoController> _logger;

        public ActividadNinoController(IActividadNinoService actividadNinoService, ILogger<ActividadNinoController> logger)
        {
            _actividadNinoService = actividadNinoService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ActividadNinoDTO>>> GetAll()
        {
            try
            {
                var relaciones = await _actividadNinoService.GetAllAsync();
                return Ok(relaciones);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener las relaciones de niño y actividad.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("{ninoId}/{actividadId}")]
        public async Task<ActionResult<ActividadNinoDTO>> GetById(Guid ninoId, Guid actividadId)
        {
            try
            {
                var relacion = await _actividadNinoService.GetByIdAsync(ninoId, actividadId);
                if (relacion == null)
                {
                    return NotFound("La relación de niño y actividad no existe.");
                }

                return Ok(relacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener la relación de niño y actividad.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ActividadNinoDTO>> Create(ActividadNinoDTO dto)
        {
            try
            {
                var createdRelacion = await _actividadNinoService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { ninoId = dto.NinoId, actividadId = dto.ActividadId }, createdRelacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la relación de niño y actividad.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpPut("{ninoId}/{actividadId}")]
        public async Task<IActionResult> Update(Guid ninoId, Guid actividadId, ActividadNinoDTO dto)
        {
            if (ninoId != dto.NinoId || actividadId != dto.ActividadId)
            {
                return BadRequest("Los identificadores proporcionados no coinciden con los de la relación.");
            }

            try
            {
                var existingRelacion = await _actividadNinoService.GetByIdAsync(ninoId, actividadId);
                if (existingRelacion == null)
                {
                    return NotFound("La relación de niño y actividad no existe.");
                }

                
                var updatedRelacion = await _actividadNinoService.CreateAsync(dto); 
                return Ok(updatedRelacion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar la relación de niño y actividad.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpDelete("{ninoId}/{actividadId}")]
        public async Task<IActionResult> Delete(Guid ninoId, Guid actividadId)
        {
            try
            {
                var success = await _actividadNinoService.DeleteAsync(ninoId, actividadId);
                if (!success)
                {
                    return NotFound("La relación de niño y actividad no existe.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar la relación de niño y actividad.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }
    }
}
