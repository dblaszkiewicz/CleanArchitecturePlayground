using CompanyManager.Domain.Repositories;

namespace CompanyManager.Infrastructure.Repositories
{
    internal sealed class UnitOfWork : IUnitOfWork
    {
        public Task CommitAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
