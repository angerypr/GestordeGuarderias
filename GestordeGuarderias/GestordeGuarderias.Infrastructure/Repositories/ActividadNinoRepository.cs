using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class ActividadNinoRepository : BaseRepository<ActividadNino>, IActividadNinoRepository
    {
        public ActividadNinoRepository(GestordeGuarderiasDbContext context) : base(context)
        {
        }

        public async Task<List<ActividadNino>> GetByActividadIdAsync(Guid actividadId)
        {
            return await _dbSet
                .Include(an => an.Nino)
                .Where(an => an.ActividadId == actividadId)
                .ToListAsync();
        }

        public async Task<List<ActividadNino>> GetByNinoIdAsync(Guid ninoId)
        {
            return await _dbSet
                .Include(an => an.Actividad)
                .Where(an => an.NinoId == ninoId)
                .ToListAsync();
        }

        public async Task<ActividadNino?> GetByIdAsync(Guid ninoId, Guid actividadId)
        {
            return await _dbSet
                .Include(an => an.Nino)
                .Include(an => an.Actividad)
                .FirstOrDefaultAsync(an => an.NinoId == ninoId && an.ActividadId == actividadId);
        }
    }
}
