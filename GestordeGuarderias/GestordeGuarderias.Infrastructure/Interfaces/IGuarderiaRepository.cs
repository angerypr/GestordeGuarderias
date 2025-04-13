using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Infrastructure.Interfaces
{
    public interface IGuarderiaRepository
    {
        Task<List<Guarderia>> GetAllAsync();
        Task<Guarderia?> GetByIdAsync(Guid id);
        Task<int> AddAsync(Guarderia guarderia);
        Task<int> UpdateAsync(Guarderia guarderia);
        Task<int> DeleteAsync(Guarderia guarderia);
        Task<bool> ExistsAsync(Guid id);
    }
}
