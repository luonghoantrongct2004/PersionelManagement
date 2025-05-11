using Microsoft.EntityFrameworkCore;

namespace BE.DAL.Reporsitory;

internal class PostgresRepositoryImpl<T> : IPostgresRepository<T> where T : class
{
    private readonly DbContext _dbContext;
    private readonly DbSet<T> _dbSet;

    public PostgresRepositoryImpl(DbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public virtual void Add(T entity)
    {
        _dbSet.Add(entity);
        _dbContext.SaveChanges();
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public virtual void Delete(Guid id)
    {
        var entity = _dbSet.Find(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            _dbContext.SaveChanges();
        }
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _dbSet.AsNoTracking().ToList();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public virtual T? GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public virtual async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual void Update(T entity)
    {
        _dbSet.Update(entity);
        _dbContext.SaveChanges();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync();
    }
}
