using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class NinoRepository : BaseRepository<Nino>, INinoRepository
    {
        public NinoRepository(GestordeGuarderiasDbContext context) : base(context)
        {
        }

        public async Task<List<Nino>> GetNinosByNameAsync(string nombre)
        {
            return await _dbSet
                .Where(n => n.Nombre.Contains(nombre) || n.Apellido.Contains(nombre))
                .ToListAsync();
        }
        public async Task<IEnumerable<Nino>> GetAllWithTutorAndGuarderiaAsync()
        {
            return await _dbSet
                .Include(n => n.Tutor)
                .Include(n => n.Guarderia)
                .ToListAsync();
        }

        public async Task<Nino?> GetByIdWithTutorAndGuarderiaAsync(Guid id)
        {
            return await _dbSet
                .Include(n => n.Tutor)
                .Include(n => n.Guarderia)
                .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
