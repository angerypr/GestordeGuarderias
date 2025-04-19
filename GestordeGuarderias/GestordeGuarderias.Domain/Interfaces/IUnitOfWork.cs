using System;
using System.Threading.Tasks;
using GestordeGuarderias.Domain.Interfaces;

namespace GestordeGuarderias.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGuarderiaRepository GuarderiaRepository { get; }
        ITutorRepository TutorRepository { get; }
        INinoRepository NinoRepository { get; }
        IActividadRepository ActividadRepository { get; }
        IPagoRepository PagoRepository { get; }
        IAsistenciaRepository AsistenciaRepository { get; }

        Task<int> SaveAsync();
        Task CompleteAsync();  
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
