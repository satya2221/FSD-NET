namespace _7._Intro_to_ASP;

public interface ITransactionRepository : IDisposable
{
    Task SaveChangesAsync();
    void ChangeTrackerClear();
    Task BeginTransactionAsync();
    Task CommitAsync();
    Task RollbackAsync();
}
