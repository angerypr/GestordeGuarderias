using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;
using GestordeGuarderias.Infrastructure.Repositories;

namespace GestordeGuarderias.Application.Services
{
    public class PagoService : IPagoService
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INinoRepository _ninoRepository;
        private readonly IGuarderiaRepository _guarderiaRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly IServicioEmail _servicioEmail;

        public PagoService(IPagoRepository pagoRepository, IUnitOfWork unitOfWork, 
            INinoRepository ninoRepository, IGuarderiaRepository guarderiaRepository, ITutorRepository tutorRepository, IServicioEmail servicioEmail)
        {
            _pagoRepository = pagoRepository;
            _unitOfWork = unitOfWork;
            _ninoRepository = ninoRepository;
            _guarderiaRepository = guarderiaRepository;
            _tutorRepository = tutorRepository;
            _servicioEmail = servicioEmail;
        }

        public async Task<IEnumerable<PagoDTO>> GetAllAsync()
        {
            var pagos = await _pagoRepository.GetAllWithRelationsAsync();

            return pagos.Select(p => new PagoDTO
            {
                Id = p.Id,
                Monto = p.Monto,
                Fecha = p.Fecha,
                NinoId = p.NinoId,
                TutorId = p.TutorId,
                GuarderiaId = p.GuarderiaId,
                Nino = new NinoDTO
                {
                    Id = p.Nino.Id,
                    Nombre = p.Nino.Nombre,
                    Apellido = p.Nino.Apellido
                },
                Tutor = new TutorDTO
                {
                    Id = p.Tutor.Id,
                    Nombre = p.Tutor.Nombre,
                    Apellido = p.Tutor.Apellido,
                    CorreoElectronico = p.Tutor.CorreoElectronico
                },
                Guarderia = new GuarderiaDTO
                {
                    Id = p.Guarderia.Id,
                    Nombre = p.Guarderia.Nombre
                }
            });
        }

        public async Task<PagoDTO?> GetByIdAsync(Guid id)
        {
            var pago = await _pagoRepository.GetByIdWithRelationsAsync(id);
            if (pago == null) return null;

            return new PagoDTO
            {
                Id = pago.Id,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                NinoId = pago.NinoId,
                TutorId = pago.TutorId,
                GuarderiaId = pago.GuarderiaId,
                Nino = new NinoDTO
                {
                    Id = pago.Nino.Id,
                    Nombre = pago.Nino.Nombre,
                    Apellido = pago.Nino.Apellido
                },
                Tutor = new TutorDTO
                {
                    Id = pago.Tutor.Id,
                    Nombre = pago.Tutor.Nombre,
                    Apellido = pago.Tutor.Apellido,
                    CorreoElectronico = pago.Tutor.CorreoElectronico
                },
                Guarderia = new GuarderiaDTO
                {
                    Id = pago.Guarderia.Id,
                    Nombre = pago.Guarderia.Nombre
                }
            };
        }

        public async Task<PagoDTO> CreateAsync(PagoDTO dto)
        {
            var nino = await _ninoRepository.GetByIdAsync(dto.NinoId);

            if (nino == null)
                throw new Exception("Niño no encontrado.");

            var guarderia = await _guarderiaRepository.GetByIdAsync(nino.GuarderiaId);

            if (guarderia == null)
                throw new Exception("Guardería no encontrada.");

            var tutor = await _tutorRepository.GetByIdAsync(nino.TutorId);

            if (tutor == null)
                throw new Exception("Tutor no encontrado.");

            var pago = new Pago
            {
                Id = Guid.NewGuid(),
                Monto = dto.Monto,
                Fecha = dto.Fecha,
                NinoId = dto.NinoId,
                Nino = nino,
                TutorId = tutor.Id,
                Tutor = tutor,
                GuarderiaId = guarderia.Id,
                Guarderia = guarderia
            };

            await _pagoRepository.AddAsync(pago);
            await _unitOfWork.CompleteAsync();

            var asunto = "Pago registrado con éxito";
            var cuerpo = $@"Hola {tutor.Nombre} {tutor.Apellido}, usted ha realizado un pago exitoso a la guardería {guarderia.Nombre} por un monto de {pago.Monto:C}.";

            await _servicioEmail.EnviarEmail(tutor.CorreoElectronico, asunto, cuerpo);

            return new PagoDTO
            {
                Id = pago.Id,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                NinoId = pago.NinoId,
                TutorId = pago.TutorId,
                GuarderiaId = pago.GuarderiaId,
                Nino = new NinoDTO { Id = nino.Id, Nombre = nino.Nombre, Apellido = nino.Apellido },
                Tutor = new TutorDTO { Id = tutor.Id, Nombre = tutor.Nombre, Apellido = tutor.Apellido, CorreoElectronico = tutor.CorreoElectronico },
                Guarderia = new GuarderiaDTO { Id = guarderia.Id, Nombre = guarderia.Nombre }
            };
        }

        public async Task<bool> UpdateAsync(Guid id, PagoDTO dto)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null) return false;

            pago.Monto = dto.Monto;
            pago.Fecha = dto.Fecha;


            await _pagoRepository.UpdateAsync(pago);
            await _unitOfWork.CompleteAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null) return false;

            await _pagoRepository.DeleteAsync(pago);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}