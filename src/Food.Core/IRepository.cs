
public interface IRepository<T> where T : class, IAggregate
{
    Task<T?> GetByIdAsync(int id, ISpecification<T>? spec = null);
    Task<List<T>> ListAsync(ISpecification<T>? spec = null);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}
