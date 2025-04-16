using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class TutorRepository : BaseRepository<Tutor>, ITutorRepository
    {
        public TutorRepository(GestordeGuarderiasDbContext context) : base(context)
        {
        }

        public async Task<List<Tutor>> GetTutorsByNameAsync(string nombre)
        {
            return await _dbSet
                .Where(t => t.Nombre.Contains(nombre) || t.Apellido.Contains(nombre))
                .ToListAsync();
        }
    }
}
