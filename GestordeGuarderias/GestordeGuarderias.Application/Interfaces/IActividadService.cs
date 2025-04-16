using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IActividadService
    {
        Task<IEnumerable<ActividadDTO>> GetAllAsync();
        Task<ActividadDTO> GetByIdAsync(Guid id);
        Task<ActividadDTO> CreateAsync(ActividadDTO dto);
        Task UpdateAsync(Guid id, ActividadDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
