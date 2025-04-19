using GestordeGuarderias.Application.DTOs;
using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IActividadService
    {
        Task<IEnumerable<ActividadDTO>> GetAllWithGuarderiaAsync();
        Task<List<Actividad>> GetActividadesByNombreAsync(string nombre);
        Task<Actividad?> GetByIdWithGuarderiaAsync(Guid id);
        Task<ActividadDTO> GetByIdAsync(Guid id);
        Task<ActividadDTO> CreateAsync(ActividadDTO dto);
        Task UpdateAsync(Guid id, ActividadDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
