using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IAsistenciaService
    {
        Task<IEnumerable<AsistenciaDTO>> GetAllAsync();
        Task<AsistenciaDTO?> GetByIdAsync(Guid id);
        Task<AsistenciaDTO> CreateAsync(AsistenciaDTO dto);
        Task<bool> UpdateAsync(Guid id, AsistenciaDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
