using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IGuarderiaService
    {
        Task<IEnumerable<GuarderiaDTO>> GetAllAsync();
        Task<GuarderiaDTO?> GetByIdAsync(Guid id);
        Task<IEnumerable<GuarderiaDTO>> GetByNameAsync(string nombre);
        Task<GuarderiaDTO> CreateAsync(GuarderiaDTO dto);
        Task<bool> UpdateAsync(Guid id, GuarderiaDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
