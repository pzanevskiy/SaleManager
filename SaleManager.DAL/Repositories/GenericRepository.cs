using SaleManager.DAL.Repositories.Interfaces;
using SaleManager.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SaleManager.DAL.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        SaleContext context;
        DbSet<TEntity> entitySet;
            
        public GenericRepository(SaleContext context)
        {
            this.context = context;
            entitySet = this.context.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get()
        {
            return entitySet.ToList();
        }

        public TEntity Get(Expression<Func<TEntity,bool>> predicate)
        {
            return entitySet.FirstOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            if (entity != null)
            {
                entitySet.Add(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (entity != null)
            {
                entitySet.Remove(entity);
            }
        }

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                context.Entry(entity).State = EntityState.Modified;
            }
        }       
    }
}
