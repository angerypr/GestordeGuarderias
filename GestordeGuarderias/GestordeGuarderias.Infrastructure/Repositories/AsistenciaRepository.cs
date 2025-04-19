// Infrastructure/Repositories/AsistenciaRepository.cs
using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;
using GestordeGuarderias.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class AsistenciaRepository : BaseRepository<Asistencia>, IAsistenciaRepository
    {
        public AsistenciaRepository(GestordeGuarderiasDbContext context) : base(context) { }

        public async Task<IEnumerable<Asistencia>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(a => a.Nino)
                .ThenInclude(n => n.Guarderia)
                .ToListAsync();
        }

        public async Task<Asistencia?> GetByIdWithRelationsAsync(Guid id)
        {
            return await _dbSet
                .Include(a => a.Nino)
                .ThenInclude(n => n.Guarderia)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
