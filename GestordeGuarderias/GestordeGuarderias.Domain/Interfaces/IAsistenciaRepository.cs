using GestordeGuarderias.Domain.Entities;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface IAsistenciaRepository : IBaseRepository<Asistencia>
    {
        Task<IEnumerable<Asistencia>> GetAllWithRelationsAsync();
        Task<Asistencia?> GetByIdWithRelationsAsync(Guid id);
    }

}
