using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GestordeGuarderias.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<int> AddAsync(T entity);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
        Task<bool> ExistsAsync(Guid id);
    }
}
