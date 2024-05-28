using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ServiceManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.EFCore.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly ApplicationDBContext db;
        internal DbSet<T> dbset;

        public RepositoryBase(ApplicationDBContext _db)
        {
            db = _db;
            this.dbset = this.db.Set<T>();
        }
        public void Add(T entity)
        {
            this.dbset.Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this.dbset.AddRange(entities);
        }

        public void Delete(T entity)
        {
            this.dbset.Remove(entity);
        }        

        public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = this.dbset;
            query = query.Where(filter);
            //if (!string.IsNullOrEmpty(includeProperties))
            //{
            //    foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            //    {
            //        query = query.Include(property);
            //    }
            //}

            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[]? includeProperties)
        {
            IQueryable<T> query = dbset;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Apply all includes
            if (includeProperties != null)
            {
                query = includeProperties.Aggregate(query, (current, include) => current.Include(include));
            }

            return query.ToList();
        }

        //public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        //{
        //    IQueryable<T> query = dbset;
        //    if (filter != null)
        //    {
        //        query = query.Where(filter);
        //    }         

        //    if (!string.IsNullOrEmpty(includeProperties))
        //    {
        //        foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        //        {
        //            query = query.Include(property.Trim());
        //        }
        //    }

        //    return query.ToList();
        //}      

        public bool save()
        {
          var  result =  this.db.SaveChanges();
            return result > 0;
        }

        public void Update(T entity)
        {
            this.dbset.Update(entity);
        }
    }
}
