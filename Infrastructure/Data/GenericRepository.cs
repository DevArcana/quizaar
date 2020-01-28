using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public abstract class GenericRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly QuizAppContext _context;

        protected GenericRepository(QuizAppContext context)
        {
            _context = context;
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<IReadOnlyCollection<TEntity>> GetAllAsync(ISpecification<TEntity> spec = null)
        {
            if (spec == null)
            {
                return await _context.Set<TEntity>().ToListAsync();
            }

            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> CountAsync(ISpecification<TEntity> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> spec)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_context.Set<TEntity>().AsQueryable(), spec);
        }
    }
}
