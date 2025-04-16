using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface ITutorService
    {
        Task<IEnumerable<TutorDTO>> GetAllAsync();
        Task<TutorDTO?> GetByIdAsync(Guid id);
        Task<TutorDTO> CreateAsync(TutorDTO dto);
        Task<bool> UpdateAsync(Guid id, TutorDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
