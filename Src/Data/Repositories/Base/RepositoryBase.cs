using Data.DB;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected DEVDbContext repositoryContext { get; set; }
        public RepositoryBase(DEVDbContext repositoryContext)
        {
            this.repositoryContext = repositoryContext;
        }
        public IQueryable<T> FindAll() => repositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) =>
            repositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => repositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => repositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => repositoryContext.Set<T>().Remove(entity);
    }
}
