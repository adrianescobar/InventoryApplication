using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InventoryApplication.Domain.Repository.Base;
using InventoryApplication.Infrastructure.Repository.Context;
using InventoryApplication.Infrastructure.Repository.Utils;

namespace InventoryApplication.Infrastructure.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly InventoryContext _context;
        protected readonly DbSet<T> _dbSet;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Repository(InventoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T?> GetByIdAsync(int id) => await _dbSet.FindAsync(id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<T>> GetAllAsync() => await _dbSet.AsNoTracking()
            .OrderByDescending(e => EF.Property<object>(e, "Id"))
            .ToListAsync();
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public virtual async Task<Paged<T>> GetAllAsync(int pageNumber, int pageSize) => 
            await _dbSet.AsNoTracking()
            .OrderByDescending(e => EF.Property<object>(e, "Id"))
            .ToPagedAsync(pageNumber, pageSize);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                throw new InvalidOperationException($"Entity with id {id} not found.");

            _dbSet.Remove(entity);
        }
    }
}
