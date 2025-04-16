using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;

namespace GestordeGuarderias.Application.Services
{
    public class GuarderiaService : IGuarderiaService
    {
        private readonly IGuarderiaRepository _guarderiaRepository;
        private readonly IUnitOfWork _unitOfWork;

        public GuarderiaService(IGuarderiaRepository guarderiaRepository, IUnitOfWork unitOfWork)
        {
            _guarderiaRepository = guarderiaRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<GuarderiaDTO>> GetAllAsync()
        {
            var guarderias = await _guarderiaRepository.GetAllAsync();

            return guarderias.Select(g => new GuarderiaDTO
            {
                Id = g.Id,
                Nombre = g.Nombre,
                Direccion = g.Direccion,
                Telefono = g.Telefono
            }).ToList();
        }

        public async Task<GuarderiaDTO?> GetByIdAsync(Guid id)
        {
            var guarderia = await _guarderiaRepository.GetByIdAsync(id);
            if (guarderia == null) return null;

            return new GuarderiaDTO
            {
                Id = guarderia.Id,
                Nombre = guarderia.Nombre,
                Direccion = guarderia.Direccion,
                Telefono = guarderia.Telefono
            };
        }

        public async Task<IEnumerable<GuarderiaDTO>> GetByNameAsync(string nombre)
        {
            var guarderias = await _guarderiaRepository.GetGuarderiasByNameAsync(nombre);
            return guarderias.Select(g => new GuarderiaDTO
            {
                Id = g.Id,
                Nombre = g.Nombre,
                Direccion = g.Direccion,
                Telefono = g.Telefono
            }).ToList();
        }

        public async Task<GuarderiaDTO> CreateAsync(GuarderiaDTO dto)
        {
            try
            {
                var guarderia = new Guarderia
                {
                    Id = Guid.NewGuid(),
                    Nombre = dto.Nombre,
                    Direccion = dto.Direccion,
                    Telefono = dto.Telefono
                };

                await _guarderiaRepository.AddAsync(guarderia);
                await _unitOfWork.CompleteAsync();

                return new GuarderiaDTO
                {
                    Id = guarderia.Id,
                    Nombre = guarderia.Nombre,
                    Direccion = guarderia.Direccion,
                    Telefono = guarderia.Telefono
                };
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public async Task<bool> UpdateAsync(Guid id, GuarderiaDTO dto)
        {
            try
            {
                var guarderia = await _guarderiaRepository.GetByIdAsync(id);
                if (guarderia == null) return false;

                guarderia.Nombre = dto.Nombre;
                guarderia.Direccion = dto.Direccion;
                guarderia.Telefono = dto.Telefono;

                await _guarderiaRepository.UpdateAsync(guarderia);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var guarderia = await _guarderiaRepository.GetByIdAsync(id);
                if (guarderia == null) return false;

                await _guarderiaRepository.DeleteAsync(guarderia);
                await _unitOfWork.CompleteAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
