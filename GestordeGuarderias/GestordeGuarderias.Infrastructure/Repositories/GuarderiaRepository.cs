using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class GuarderiaRepository : BaseRepository<Guarderia>, IGuarderiaRepository
    {
        public GuarderiaRepository(GestordeGuarderiasDbContext context) : base(context)
        {
        }
        public async Task<List<Guarderia>> GetGuarderiasByNameAsync(string nombre)
        {
            return await _dbSet
                .Where(g => g.Nombre.Contains(nombre))
                .ToListAsync();
        }

    }
}
