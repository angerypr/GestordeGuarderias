using GestordeGuarderias.Application.DTOs;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IPagoService
    {
        Task<IEnumerable<PagoDTO>> GetAllAsync();
        Task<PagoDTO?> GetByIdAsync(Guid id);
        Task<PagoDTO> CreateAsync(PagoDTO dto);
        Task<bool> UpdateAsync(Guid id, PagoDTO dto);
        Task<bool> DeleteAsync(Guid id);

    }
}
