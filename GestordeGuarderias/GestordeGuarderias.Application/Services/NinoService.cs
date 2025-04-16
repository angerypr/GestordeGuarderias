using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;
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
                GuarderiaId = n.GuarderiaId
            });
        }

        public async Task<NinoDTO?> GetByIdAsync(Guid id)
        {
            var nino = await _ninoRepository.GetByIdAsync(id);
            if (nino == null) return null;

            return new NinoDTO
            {
                Id = nino.Id,
                Nombre = nino.Nombre,
                Apellido = nino.Apellido,
                FechaNacimiento = nino.FechaNacimiento,
                TutorId = nino.TutorId,
                GuarderiaId = nino.GuarderiaId
            };
        }
        public async Task<NinoDTO> CreateAsync(NinoDTO dto)
        {
            Console.WriteLine($">>> TutorId recibido en el DTO: {dto.TutorId}");
            var tutor = await _tutorRepository.GetByIdAsync(dto.TutorId);
            var guarderia = await _guarderiaRepository.GetByIdAsync(dto.GuarderiaId);

            if (tutor == null || guarderia == null)
                throw new Exception("Tutor o guardería no encontrados.");

            var nino = new Nino
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                FechaNacimiento = dto.FechaNacimiento,
                TutorId = dto.TutorId,
                GuarderiaId = dto.GuarderiaId
            };

            await _ninoRepository.AddAsync(nino);
            await _unitOfWork.CompleteAsync();
            Console.WriteLine($"Correo del tutor: {tutor.CorreoElectronico}");

            try
            {
                var asunto = "Registro en guardería";
                var cuerpo = $"Hola, {tutor.Nombre} {tutor.Apellido}. " +
                             $"Su hijo/a {nino.Nombre} {nino.Apellido} ha sido registrado en la guardería {guarderia.Nombre}.";

                Console.WriteLine(">> Enviando correo...");
                Console.WriteLine($">> Para: {tutor.CorreoElectronico}");
                Console.WriteLine($">> Asunto: {asunto}");
                Console.WriteLine($">> Cuerpo: {cuerpo}");

                await _servicioEmail.EnviarEmail(tutor.CorreoElectronico, asunto, cuerpo);
                Console.WriteLine(">> Correo enviado.");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "Error al enviar el correo electrónico de registro.");
                Console.WriteLine($"ERROR EMAIL: {ex.Message}");
            }

            return new NinoDTO
            {
                Id = nino.Id,
                Nombre = nino.Nombre,
                Apellido = nino.Apellido,
                FechaNacimiento = nino.FechaNacimiento,
                TutorId = nino.TutorId,
                GuarderiaId = nino.GuarderiaId
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
