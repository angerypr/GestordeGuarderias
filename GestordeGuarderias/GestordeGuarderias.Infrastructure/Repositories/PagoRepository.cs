using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class PagoRepository : BaseRepository<Pago>, IPagoRepository
    {
        public PagoRepository(GestordeGuarderiasDbContext context) : base(context)
        {
        }
    }
}
