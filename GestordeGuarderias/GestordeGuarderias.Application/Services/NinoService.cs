using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;
using GestordeGuarderias.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace GestordeGuarderias.Application.Services
{
    public class NinoService : INinoService
    {
        private readonly INinoRepository _ninoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IServicioEmail _servicioEmail;
        private readonly ITutorRepository _tutorRepository;
        private readonly IGuarderiaRepository _guarderiaRepository;
        private readonly ILogger<NinoService>? _logger;

        public NinoService(
            INinoRepository ninoRepository,
            IUnitOfWork unitOfWork,
            IServicioEmail servicioEmail,
            ITutorRepository tutorRepository,
            IGuarderiaRepository guarderiaRepository,
            ILogger<NinoService>? logger = null)
        {
            _ninoRepository = ninoRepository;
            _unitOfWork = unitOfWork;
            _servicioEmail = servicioEmail;
            _tutorRepository = tutorRepository;
            _guarderiaRepository = guarderiaRepository;
            _logger = logger;
        }

        public async Task<IEnumerable<NinoDTO>> GetAllAsync()
        {
            var ninos = await _ninoRepository.GetAllAsync();
            return ninos.Select(n => new NinoDTO
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Apellido = n.Apellido,
                FechaNacimiento = n.FechaNacimiento,
                TutorId = n.TutorId,
                Tutor = n.Tutor != null
                    ? new TutorDTO
                    {
                        Id = n.Tutor.Id,
                        Nombre = n.Tutor.Nombre,
                        Apellido = n.Tutor.Apellido
                    }
                    : null,
                GuarderiaId = n.GuarderiaId
            });
        }

        public async Task<NinoDTO?> GetByIdWithTutorAndGuarderiaAsync(Guid id)
        {
            var nino = await _ninoRepository.GetByIdWithTutorAndGuarderiaAsync(id);
            if (nino == null) return null;

            return new NinoDTO
            {
                Id = nino.Id,
                Nombre = nino.Nombre,
                Apellido = nino.Apellido,
                FechaNacimiento = nino.FechaNacimiento,
                TutorId = nino.TutorId,
                GuarderiaId = nino.GuarderiaId,
                Tutor = nino.Tutor != null ? new TutorDTO
                {
                    Id = nino.Tutor.Id,
                    Nombre = nino.Tutor.Nombre,
                    Apellido = nino.Tutor.Apellido
                } : null,
                Guarderia = nino.Guarderia != null ? new GuarderiaDTO
                {
                    Id = nino.Guarderia.Id,
                    Nombre = nino.Guarderia.Nombre
                } : null
            };
        }

        public async Task<IEnumerable<NinoDTO>> GetAllWithTutorAndGuarderiaAsync()
        {
            var ninos = await _ninoRepository.GetAllWithTutorAndGuarderiaAsync();

            return ninos.Select(n => new NinoDTO
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Apellido = n.Apellido,
                FechaNacimiento = n.FechaNacimiento,
                TutorId = n.TutorId,
                GuarderiaId = n.GuarderiaId,
                Tutor = n.Tutor != null ? new TutorDTO
                {
                    Id = n.Tutor.Id,
                    Nombre = n.Tutor.Nombre,
                    Apellido = n.Tutor.Apellido
                } : null,
                Guarderia = n.Guarderia != null ? new GuarderiaDTO
                {
                    Id = n.Guarderia.Id,
                    Nombre = n.Guarderia.Nombre
                } : null
            });
        }

        public async Task<IEnumerable<NinoDTO>> GetNinosByNameAsync(string nombre)
        {
            var ninos = await _ninoRepository.GetNinosByNameAsync(nombre);

            return ninos.Select(n => new NinoDTO
            {
                Id = n.Id,
                Nombre = n.Nombre,
                Apellido = n.Apellido,
                FechaNacimiento = n.FechaNacimiento,
                TutorId = n.TutorId,
                GuarderiaId = n.GuarderiaId
            });
        }
        public async Task<NinoDTO> CreateAsync(NinoDTO dto)
        {
            var tutor = await _tutorRepository.GetByIdAsync(dto.TutorId);
            if (tutor == null)
                throw new Exception("Tutor no encontrado.");

            var guarderia = await _guarderiaRepository.GetByIdAsync(dto.GuarderiaId);
            if (guarderia == null)
                throw new Exception("Guardería no encontrada.");

            var nino = new Nino
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                TutorId = tutor.Id,
                GuarderiaId = guarderia.Id,
                Tutor = tutor,
                Guarderia = guarderia
            };

            await _ninoRepository.AddAsync(nino);
            await _unitOfWork.CompleteAsync();

            try
            {
                var asunto = "Registro en guardería";
                var cuerpo = $@"Hola {tutor.Nombre} {tutor.Apellido}, 
Su hijo/a {nino.Nombre} {nino.Apellido} ha sido registrado en la guardería {guarderia.Nombre}.";

                await _servicioEmail.EnviarEmail(tutor.CorreoElectronico, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error al enviar el correo electrónico de registro del niño.");
            }

            return new NinoDTO
            {
                Id = nino.Id,
                Nombre = nino.Nombre,
                Apellido = nino.Apellido,
                FechaNacimiento = nino.FechaNacimiento,
                TutorId = tutor.Id,
                GuarderiaId = guarderia.Id,
                Tutor = new TutorDTO
                {
                    Id = tutor.Id,
                    Nombre = tutor.Nombre,
                    Apellido = tutor.Apellido,
                    CorreoElectronico = tutor.CorreoElectronico
                },
                Guarderia = new GuarderiaDTO
                {
                    Id = guarderia.Id,
                    Nombre = guarderia.Nombre
                }
            };
        }

        public async Task<bool> UpdateAsync(Guid id, NinoDTO dto)
        {
            var nino = await _ninoRepository.GetByIdAsync(id);
            if (nino == null) return false;

            nino.Nombre = dto.Nombre;
            nino.Apellido = dto.Apellido;
            nino.FechaNacimiento = dto.FechaNacimiento;
            nino.TutorId = dto.TutorId;
            nino.GuarderiaId = dto.GuarderiaId;

            await _ninoRepository.UpdateAsync(nino);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var nino = await _ninoRepository.GetByIdAsync(id);
            if (nino == null) return false;

            await _ninoRepository.DeleteAsync(nino);
            await _unitOfWork.CompleteAsync();

            return true;
        }

    }
}
