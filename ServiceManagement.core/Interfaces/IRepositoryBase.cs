using ServiceManagement.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ServiceManagement.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        //IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
        T Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        Task<PagedResponseOffset<T>> GetWithOffsetPagination(int pageNumber, int pageSize);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);        
        void Delete(T entity);
        bool save();
    }
}
