using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface INinoService
    {
        Task<IEnumerable<NinoDTO>> GetAllAsync();
        Task<NinoDTO> CreateAsync(NinoDTO dto);
        Task<bool> UpdateAsync(Guid id, NinoDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<NinoDTO>> GetAllWithTutorAndGuarderiaAsync();
        Task<NinoDTO?> GetByIdWithTutorAndGuarderiaAsync(Guid id);
        Task<IEnumerable<NinoDTO>> GetNinosByNameAsync(string nombre);
    }
}
