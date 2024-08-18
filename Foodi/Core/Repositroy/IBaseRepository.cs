using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Foodi.Core.Repositroy
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById( int id);

        TResult GetById<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> selector);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(string[] includes = null);

        Task<IEnumerable<TResult>> GetAll<TResult>(Expression<Func<T, bool>> criteria, Expression<Func<T, TResult>> selector);
       

        T Find(Expression<Func<T, bool>> criteria, string[] includes = null);

        Task<IEnumerable<T>> FindAllAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        bool Any(Expression<Func<T, bool>> criteria);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entity);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> criteria);
    }
}
