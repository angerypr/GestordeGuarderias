using GestordeGuarderias.Application.Interfaces;
using GestordeGuarderias.Domain.Interfaces;
using GestordeGuarderias.Infrastructure;
using GestordeGuarderias.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly GestordeGuarderiasDbContext _context;

    public IGuarderiaRepository GuarderiaRepository { get; }
    public ITutorRepository TutorRepository { get; }
    public INinoRepository NinoRepository { get; }
    public IActividadRepository ActividadRepository { get; }
    public IPagoRepository PagoRepository { get; }
    public IActividadNinoRepository ActividadNinoRepository { get; }
    public IAsistenciaRepository AsistenciaRepository { get; }

    public UnitOfWork(
        GestordeGuarderiasDbContext context,
        IGuarderiaRepository guarderias,
        ITutorRepository tutores,
        INinoRepository ninos,
        IActividadRepository actividades,
        IPagoRepository pagos,
        IActividadNinoRepository actividadesninos,
        IAsistenciaRepository asistencias)
    {
        _context = context;
        GuarderiaRepository = guarderias;
        TutorRepository = tutores;
        NinoRepository = ninos;
        ActividadRepository = actividades;
        PagoRepository = pagos;
        ActividadNinoRepository = actividadesninos;
        AsistenciaRepository = asistencias;
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public async Task CompleteAsync()
    {
        await SaveAsync();
    }

    public async Task BeginTransactionAsync()
    {
        await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _context.Database.CommitTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _context.Database.RollbackTransactionAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
