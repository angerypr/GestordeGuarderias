using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;
using GestordeGuarderias.Infrastructure.Repositories;

namespace GestordeGuarderias.Application.Services
{
    public class ActividadService : IActividadService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IActividadRepository _actividadRepository;

        public ActividadService(IUnitOfWork unitOfWork, IActividadRepository actividadRepository)
        {
            _unitOfWork = unitOfWork;
            _actividadRepository = actividadRepository;
        }

        public async Task<IEnumerable<ActividadDTO>> GetAllWithGuarderiaAsync()
        {
            var actividades = await _actividadRepository.GetAllWithGuarderiaAsync();

            return actividades.Select(a => new ActividadDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Descripcion = a.Descripcion,
                Fecha = a.Fecha,
                Hora = a.Hora,
                GuarderiaId = a.GuarderiaId,
                Guarderia = a.Guarderia != null
                    ? new GuarderiaDTO
                    {
                        Id = a.Guarderia.Id,
                        Nombre = a.Guarderia.Nombre
                    }
                    : new GuarderiaDTO
                    {
                        Id = Guid.Empty,
                        Nombre = "Guardería no disponible"
                    }
            }).ToList();
        }

        public async Task<ActividadDTO> GetByIdAsync(Guid id)
        {
            var actividad = await _actividadRepository.GetByIdWithGuarderiaAsync(id)
                ?? throw new Exception("Actividad no encontrada");

            return new ActividadDTO
            {
                Id = actividad.Id,
                Nombre = actividad.Nombre,
                Descripcion = actividad.Descripcion,
                Fecha = actividad.Fecha,
                Hora = actividad.Hora,
                GuarderiaId = actividad.GuarderiaId,
                Guarderia = actividad.Guarderia != null
                    ? new GuarderiaDTO
                    {
                        Id = actividad.Guarderia.Id,
                        Nombre = actividad.Guarderia.Nombre
                    }
                    : null
            };
        }

        public async Task<Actividad?> GetByIdWithGuarderiaAsync(Guid id)
        {
            return await _actividadRepository.GetByIdWithGuarderiaAsync(id);
        }

        public async Task<List<Actividad>> GetActividadesByNombreAsync(string nombre)
        {
            return await _actividadRepository.GetActividadesByNombreAsync(nombre);
        }

        public async Task<ActividadDTO> CreateAsync(ActividadDTO dto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var actividad = new Actividad
                {
                    Id = Guid.NewGuid(),
                    Nombre = dto.Nombre,
                    Descripcion = dto.Descripcion,
                    Fecha = dto.Fecha,
                    Hora = dto.Hora,
                    GuarderiaId = dto.GuarderiaId
                };

                await _actividadRepository.AddAsync(actividad);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return new ActividadDTO
                {
                    Id = actividad.Id,
                    Nombre = actividad.Nombre,
                    Descripcion = actividad.Descripcion,
                    Fecha = actividad.Fecha,
                    Hora = actividad.Hora,
                    GuarderiaId = actividad.GuarderiaId
                };
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Error al crear la actividad", ex);
            }
        }

        public async Task UpdateAsync(Guid id, ActividadDTO dto)
        {
            var actividad = await _actividadRepository.GetByIdAsync(id);
            if (actividad == null)
                throw new Exception("Actividad no encontrada");

            actividad.Nombre = dto.Nombre;
            actividad.Descripcion = dto.Descripcion;
            actividad.Fecha = dto.Fecha;
            actividad.Hora = dto.Hora;
            actividad.GuarderiaId = dto.GuarderiaId;

            await _actividadRepository.UpdateAsync(actividad);
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CommitTransactionAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var actividad = await _actividadRepository.GetByIdAsync(id);
                if (actividad == null) return false;

                await _actividadRepository.DeleteAsync(actividad);
                await _unitOfWork.CompleteAsync();
                await _unitOfWork.CommitTransactionAsync();

                return true;
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }
    }
}
