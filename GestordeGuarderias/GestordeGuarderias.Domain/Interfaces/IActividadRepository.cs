using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface IActividadRepository : IBaseRepository<Actividad>
    {
        Task<List<Actividad>> GetActividadesByNombreAsync(string nombre);
    }
}
