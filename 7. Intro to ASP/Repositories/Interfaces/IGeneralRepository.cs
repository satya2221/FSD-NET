namespace _7._Intro_to_ASP;

public interface IGeneralRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task<TEntity> CreateAsync(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
}
