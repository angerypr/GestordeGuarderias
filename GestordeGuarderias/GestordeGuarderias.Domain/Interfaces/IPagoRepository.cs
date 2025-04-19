using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface IPagoRepository : IBaseRepository<Pago>
    {
        Task<IEnumerable<Pago>> GetAllWithRelationsAsync();
        Task<Pago?> GetByIdWithRelationsAsync(Guid id);
    }
}
