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
            var pagos = await _pagoRepository.GetAllAsync();
            return pagos.Select(p => new PagoDTO
            {
                Id = p.Id,
                Monto = p.Monto,
                Fecha = p.Fecha,
                NinoId = p.NinoId,
                TutorId = p.TutorId,
                GuarderiaId = p.GuarderiaId
            });
        }

        public async Task<PagoDTO?> GetByIdAsync(Guid id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null) return null;

            return new PagoDTO
            {
                Id = pago.Id,
                Monto = pago.Monto,
                Fecha = pago.Fecha,
                NinoId = pago.NinoId,
                TutorId = pago.TutorId,
                GuarderiaId = pago.GuarderiaId
            };
        }

        public async Task<PagoDTO> CreateAsync(PagoDTO dto)
        {
            var nino = await _ninoRepository.GetByIdAsync(dto.NinoId);
            var guarderia = await _guarderiaRepository.GetByIdAsync(dto.GuarderiaId);
            var tutor = await _tutorRepository.GetByIdAsync(dto.TutorId);

            if (nino == null || guarderia == null || tutor == null)
                throw new Exception("Entidad relacionada no encontrada");

            var pago = new Pago
            {
                Id = Guid.NewGuid(),
                Monto = dto.Monto,
                Fecha = dto.Fecha,
                NinoId = dto.NinoId,
                Nino = nino,
                TutorId = dto.TutorId,
                GuarderiaId = dto.GuarderiaId,
                Guarderia = guarderia,
                Tutor = tutor
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
                GuarderiaId = pago.GuarderiaId
            };
        }

        public async Task<bool> UpdateAsync(Guid id, PagoDTO dto)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);
            if (pago == null) return false;

            pago.Monto = dto.Monto;
            pago.Fecha = dto.Fecha;
            pago.NinoId = dto.NinoId;
            pago.TutorId = dto.TutorId;
            pago.GuarderiaId = dto.GuarderiaId;

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
