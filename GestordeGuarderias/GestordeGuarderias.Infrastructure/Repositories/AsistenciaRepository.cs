using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class AsistenciaRepository : BaseRepository<Asistencia>, IAsistenciaRepository
    {
        public AsistenciaRepository(GestordeGuarderiasDbContext context) : base(context)
        {
        }
    }
}
