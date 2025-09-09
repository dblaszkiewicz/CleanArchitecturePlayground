
namespace CompanyManager.Domain.Repositories
{
    public interface IUnitOfWork
    {
        Task CommitAsync(CancellationToken ct);
    }
}
