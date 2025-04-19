using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Domain.Interfaces;
using GestordeGuarderias.Infrastructure.Core;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class PagoRepository : BaseRepository<Pago>, IPagoRepository
    {
        public PagoRepository(GestordeGuarderiasDbContext context) : base(context) { }

        public async Task<IEnumerable<Pago>> GetAllWithRelationsAsync()
        {
            return await _dbSet
                .Include(p => p.Nino)                
                .ThenInclude(p => p.Tutor)           
                .Include(p => p.Nino.Guarderia)     
                .ToListAsync();
        }

        public async Task<Pago?> GetByIdWithRelationsAsync(Guid id)
        {
            return await _dbSet
                .Include(p => p.Nino)                
                .ThenInclude(p => p.Tutor)           
                .Include(p => p.Nino.Guarderia)     
                .FirstOrDefaultAsync(p => p.Id == id); 
        }
    }
}
