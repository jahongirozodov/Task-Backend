using System.Linq.Expressions;
using Task.Domain.Commons;

namespace Task.Data.IRepositories;

public interface IRepository<T> where T : Auditable
{
    ValueTask CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Destroy(T entity);
    Task<T> GetAsync(Expression<Func<T, bool>> expression, string[] includes = null);
    IQueryable<T> GetAll(Expression<Func<T, bool>> expression = null, bool IsNoTracked = true, string[] includes = null);
    ValueTask SaveAsync();
}
