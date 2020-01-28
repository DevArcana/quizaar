using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(long id);
        Task<IReadOnlyCollection<TEntity>> GetAllAsync([AllowNull] ISpecification<TEntity> spec = null);
        
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task<bool> DeleteAsync(TEntity entity);
        
        Task<int> CountAsync(ISpecification<TEntity> spec);
    }
}
