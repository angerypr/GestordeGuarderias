using GestordeGuarderias.Domain.Entities;
using GestordeGuarderias.Infrastructure.Core;
using GestordeGuarderias.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestordeGuarderias.Infrastructure.Repositories
{
    public class MensajeRepository : BaseRepository<Mensaje>, IMensajeRepository
    {
        public MensajeRepository(DbContext context) : base(context)
        {
        }
    }
}
