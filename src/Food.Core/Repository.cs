using Microsoft.EntityFrameworkCore;
using Food.Core.Model;
public class Repository<T> : IRepository<T> where T : class, IAggregate
{
    private readonly FoodDeliveryContext _context;

    public Repository(FoodDeliveryContext context)
    {
        _context = context;
    }

    public async Task<T?> GetByIdAsync(int id, ISpecification<T>? spec = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (spec != null)
        {
            query = SpecificationEvaluator<T>.GetQuery(query, spec);
        }

        return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
    }

    public async Task<List<T>> ListAsync(ISpecification<T>? spec = null)
    {
        IQueryable<T> query = _context.Set<T>();

        if (spec != null)
        {
            query = SpecificationEvaluator<T>.GetQuery(query, spec);
        }

        return await query.ToListAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(T entity)
    {
        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }
}
