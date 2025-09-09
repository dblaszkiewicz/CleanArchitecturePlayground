using CompanyManager.Domain.Repositories;

namespace CompanyManager.Domain.Services
{
    public interface IEmployeeRecordNumberService
    {
        Task<TResult> GenerateNextNumberAndExecuteAsync<TResult>(IEmployeesRepository employeesRepository, Func<int, CancellationToken, Task<TResult>> operationToExecute, CancellationToken cancellationToken);
    }
}
