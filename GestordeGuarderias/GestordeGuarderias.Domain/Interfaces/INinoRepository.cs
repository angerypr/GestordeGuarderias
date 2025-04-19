using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface INinoRepository : IBaseRepository<Nino>
    {
        Task<List<Nino>> GetNinosByNameAsync(string nombre);
        Task<IEnumerable<Nino>> GetAllWithTutorAndGuarderiaAsync();
        Task<Nino?> GetByIdWithTutorAndGuarderiaAsync(Guid id);
    }
}
