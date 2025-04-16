using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface IActividadNinoRepository : IBaseRepository<ActividadNino>
    {
        Task<List<ActividadNino>> GetByActividadIdAsync(Guid actividadId);
        Task<List<ActividadNino>> GetByNinoIdAsync(Guid ninoId);
        Task<ActividadNino?> GetByIdAsync(Guid ninoId, Guid actividadId);
    }
}
