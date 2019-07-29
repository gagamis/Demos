
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext Context { get; private set; }

        public Repository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Find T by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T</returns>
        public Task<T> GetAsync(Guid id) => Context.Set<T>().FindAsync(id);

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>DbSet<T></returns>
        public DbSet<T> GetAll() => Context.Set<T>();

        /// <summary>
        /// Finde
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>IQueryable<T></returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => Context.Set<T>().Where(predicate);

        /// <summary>
        /// Add new entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity) => Context.Set<T>().Add(entity);

        /// <summary>
        /// Add new multiple entities
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<T> entities) => Context.Set<T>().AddRange(entities);

        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(T entity) => Context.Set<T>().Remove(entity);

        /// <summary>
        /// Remove multiple entities
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<T> entities) => Context.Set<T>().RemoveRange(entities);
    }
}
