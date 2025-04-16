using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace GestordeGuarderias.Application.Services
{
    public class ActividadNinoService : IActividadNinoService
    {
        private readonly IActividadNinoRepository _actividadNinoRepository;
        private readonly INinoRepository _ninoRepository;
        private readonly IActividadRepository _actividadRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly IGuarderiaRepository _guarderiaRepository;
        private readonly IServicioEmail _servicioEmail;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ActividadNinoService>? _logger;

        public ActividadNinoService(
            IActividadNinoRepository actividadNinoRepository,
            INinoRepository ninoRepository,
            IActividadRepository actividadRepository,
            ITutorRepository tutorRepository,
            IGuarderiaRepository guarderiaRepository,
            IServicioEmail servicioEmail,
            IUnitOfWork unitOfWork,
            ILogger<ActividadNinoService>? logger = null)
        {
            _actividadNinoRepository = actividadNinoRepository;
            _ninoRepository = ninoRepository;
            _actividadRepository = actividadRepository;
            _tutorRepository = tutorRepository;
            _guarderiaRepository = guarderiaRepository;
            _servicioEmail = servicioEmail;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IEnumerable<ActividadNinoDTO>> GetAllAsync()
        {
            var relaciones = await _actividadNinoRepository.GetAllAsync();

            return relaciones.Select(r => new ActividadNinoDTO
            {
                NinoId = r.NinoId,
                ActividadId = r.ActividadId
            });
        }

        public async Task<ActividadNinoDTO?> GetByIdAsync(Guid ninoId, Guid actividadId)
        {
            var relacion = await _actividadNinoRepository.GetByIdAsync(ninoId, actividadId);
            if (relacion == null) return null;

            return new ActividadNinoDTO
            {
                NinoId = relacion.NinoId,
                ActividadId = relacion.ActividadId
            };
        }

        public async Task<ActividadNinoDTO> CreateAsync(ActividadNinoDTO dto)
        {
            var nino = await _ninoRepository.GetByIdAsync(dto.NinoId);
            var actividad = await _actividadRepository.GetByIdAsync(dto.ActividadId);

            if (nino == null || actividad == null)
                throw new Exception("Niño o actividad no encontrados.");

            var relacion = new ActividadNino
            {
                NinoId = dto.NinoId,
                ActividadId = dto.ActividadId
            };

            await _actividadNinoRepository.AddAsync(relacion);
            await _unitOfWork.CompleteAsync();

            try
            {
                var tutor = await _tutorRepository.GetByIdAsync(nino.TutorId);
                var guarderia = await _guarderiaRepository.GetByIdAsync(nino.GuarderiaId);

                if (tutor != null && guarderia != null)
                {
                    var asunto = "Registro de actividad";
                    var cuerpo = $"Hola, {tutor.Nombre} {tutor.Apellido}. Su hijo/a {nino.Nombre} {nino.Apellido} ha sido registrado en la actividad \"{actividad.Nombre}\" en la guardería {guarderia.Nombre}.";

                    await _servicioEmail.EnviarEmail(tutor.CorreoElectronico, asunto, cuerpo);
                }
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error al enviar el correo de registro de actividad.");
            }

            return new ActividadNinoDTO
            {
                NinoId = dto.NinoId,
                ActividadId = dto.ActividadId
            };
        }

        public async Task<bool> DeleteAsync(Guid ninoId, Guid actividadId)
        {
            var relacion = await _actividadNinoRepository.GetByIdAsync(ninoId, actividadId);
            if (relacion == null) return false;

            await _actividadNinoRepository.DeleteAsync(relacion);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
