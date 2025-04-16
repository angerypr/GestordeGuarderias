using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class ActividadRepository : BaseRepository<Actividad>, IActividadRepository
    {
        public ActividadRepository(GestordeGuarderiasDbContext context) : base(context)
        {
        }

        public async Task<List<Actividad>> GetActividadesByNombreAsync(string nombre)
        {
            return await _dbSet
                .Where(a => a.Nombre.Contains(nombre))
                .ToListAsync();
        }

    }
}
