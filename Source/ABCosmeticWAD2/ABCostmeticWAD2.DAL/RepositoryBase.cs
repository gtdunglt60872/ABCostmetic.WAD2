using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using ABCostmeticWAD2.DAL.Interfaces;

namespace ABCostmeticWAD2.DAL
{
    public abstract class RepositoryBase<TC, T> : IRepository<T>
        where T : class
        where TC : DbContext, new()
    {
        public TC Context { get; set; } = new TC();


        public IQueryable<T> GetAll()
        {
            var query = Context.Set<T>();
            return query;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            var query = Context.Set<T>().Where(predicate);
            return query;
        }

        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            //Context.Entry(entity).State = EntityState.Modified;
            Context.Set<T>().AddOrUpdate(entity);
        }

        public void Save()
        {
            Context.SaveChanges();
        }
    }
}
