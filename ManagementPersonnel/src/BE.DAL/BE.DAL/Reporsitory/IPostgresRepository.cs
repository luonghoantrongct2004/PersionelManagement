namespace BE.DAL.Reporsitory;

public interface IPostgresRepository<T>
{
    // Asynchronous methods
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(Guid id);

    // Synchronous methods
    IEnumerable<T> GetAll();
    T? GetById(Guid id);
    void Add(T entity);
    void Update(T entity);
    void Delete(Guid id);
}
