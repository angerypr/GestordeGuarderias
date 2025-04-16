using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IActividadNinoService
    {
        Task<IEnumerable<ActividadNinoDTO>> GetAllAsync();
        Task<ActividadNinoDTO?> GetByIdAsync(Guid ninoId, Guid actividadId);
        Task<ActividadNinoDTO> CreateAsync(ActividadNinoDTO dto);
        Task<bool> DeleteAsync(Guid ninoId, Guid actividadId);
    }
}
