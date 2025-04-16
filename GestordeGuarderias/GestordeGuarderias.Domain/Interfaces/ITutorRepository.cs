using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface ITutorRepository : IBaseRepository<Tutor>
    {
        Task<List<Tutor>> GetTutorsByNameAsync(string nombre);
    }
}
