using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface IGuarderiaRepository : IBaseRepository<Guarderia>
    {
        Task<List<Guarderia>> GetGuarderiasByNameAsync(string nombre);
    }
}
