using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface IActividadRepository : IBaseRepository<Actividad>
    {
        Task<List<Actividad>> GetActividadesByNombreAsync(string nombre);
        Task<IEnumerable<Actividad>> GetAllWithGuarderiaAsync();
        Task<Actividad?> GetByIdWithGuarderiaAsync(Guid id);
    }
}
