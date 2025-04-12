using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace GestordeGuarderias.Infrastructure.Core
{
    public class UnitOfWork : IDisposable
    {
        private readonly GestordeGuarderiasDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(GestordeGuarderiasDbContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
