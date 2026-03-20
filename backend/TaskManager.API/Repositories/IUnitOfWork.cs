namespace TaskManager.API.Repositories;

public interface IUnitOfWork : IDisposable
{
    Task<int> Commit();
}
