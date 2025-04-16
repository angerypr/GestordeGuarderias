using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface INinoService
    {
        Task<IEnumerable<NinoDTO>> GetAllAsync();
        Task<NinoDTO?> GetByIdAsync(Guid id);
        Task<NinoDTO> CreateAsync(NinoDTO dto);
        Task<bool> UpdateAsync(Guid id, NinoDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
