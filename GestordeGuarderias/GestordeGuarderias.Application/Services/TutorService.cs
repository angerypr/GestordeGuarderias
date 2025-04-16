using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;

namespace GestordeGuarderias.Application.Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _tutorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TutorService(ITutorRepository tutorRepository, IUnitOfWork unitOfWork)
        {
            _tutorRepository = tutorRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TutorDTO>> GetAllAsync()
        {
            var tutores = await _tutorRepository.GetAllAsync();
            return tutores.Select(t => new TutorDTO
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Apellido = t.Apellido,
                Telefono = t.Telefono,
                Cedula = t.Cedula,
                CorreoElectronico = t.CorreoElectronico
            });
        }

        public async Task<TutorDTO?> GetByIdAsync(Guid id)
        {
            var tutor = await _tutorRepository.GetByIdAsync(id);
            if (tutor == null) return null;

            return new TutorDTO
            {
                Id = tutor.Id,
                Nombre = tutor.Nombre,
                Apellido = tutor.Apellido,
                Telefono = tutor.Telefono,
                Cedula = tutor.Cedula,
                CorreoElectronico = tutor.CorreoElectronico
            };
        }

        public async Task<IEnumerable<TutorDTO>> GetByNameAsync(string nombre)
        {
            var tutores = await _tutorRepository.GetTutorsByNameAsync(nombre);
            return tutores.Select(t => new TutorDTO
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Apellido = t.Apellido,
                Telefono = t.Telefono,
                Cedula = t.Cedula,
                CorreoElectronico = t.CorreoElectronico
            });
        }

        public async Task<TutorDTO> CreateAsync(TutorDTO dto)
        {
            var tutor = new Tutor
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Cedula = dto.Cedula,
                CorreoElectronico = dto.CorreoElectronico,
                Ninos = new List<Nino>()
            };

            await _tutorRepository.AddAsync(tutor);
            await _unitOfWork.CompleteAsync();

            return new TutorDTO
            {
                Id = tutor.Id,
                Nombre = tutor.Nombre,
                Apellido = tutor.Apellido,
                Telefono = tutor.Telefono,
                Cedula = tutor.Cedula,
                CorreoElectronico = tutor.CorreoElectronico
            };
        }

        public async Task<bool> UpdateAsync(Guid id, TutorDTO dto)
        {
            var tutor = await _tutorRepository.GetByIdAsync(id);
            if (tutor == null) return false;

            tutor.Nombre = dto.Nombre;
            tutor.Apellido = dto.Apellido;
            tutor.Telefono = dto.Telefono;
            tutor.Cedula = dto.Cedula;
            tutor.CorreoElectronico = dto.CorreoElectronico;

            await _tutorRepository.UpdateAsync(tutor);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var tutor = await _tutorRepository.GetByIdAsync(id);
            if (tutor == null) return false;

            await _tutorRepository.DeleteAsync(tutor);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
