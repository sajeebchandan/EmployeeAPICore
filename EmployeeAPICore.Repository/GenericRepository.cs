using EmployeeAPICore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace EmployeeAPICore.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly EmployeeAPICoreDbContext _context;
        internal DbSet<TEntity> DbSet;
        public GenericRepository(EmployeeAPICoreDbContext context)
        {
            _context = context;
            DbSet = context.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet;
        }
        public virtual IEnumerable<TEntity> GetAllIncluding(params string[] includeProperties)
        {
            IQueryable<TEntity> query = DbSet;
            if (query != null)
            {
                if (query.ToList().Count() > 0)
                {
                    foreach (var includeProperty in includeProperties)
                    {
                        query.Include(includeProperty);
                    }
                }
            }
            return query;
        }
        public virtual IEnumerable<TEntity> GetAllSorted<TType>(Expression<Func<TEntity, TType>> sortCondition, bool sortDesc)
        {
            if (sortDesc)
            {
                return DbSet.OrderByDescending(sortCondition);
            }
            else
            {
                return DbSet.OrderBy(sortCondition);
            }
        }
        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }
        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }
        public virtual void Delete(object id)
        {
            Delete(DbSet.Find(id));
        }
        public virtual bool Delete(TEntity entityToDelete)
        {
            bool isSuccess = false;
            try
            {
                if (_context.Entry(entityToDelete).State == EntityState.Detached)
                {
                    DbSet.Attach(entityToDelete);
                }
                DbSet.Remove(entityToDelete);
                isSuccess = true;
            }
            catch (Exception)
            {
                isSuccess = true;
            }
            return isSuccess;
        }
        public virtual IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Where(condition);
        }
        public virtual bool Any(Expression<Func<TEntity, bool>> condition)
        {
            return DbSet.Any(condition);
        }

        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
