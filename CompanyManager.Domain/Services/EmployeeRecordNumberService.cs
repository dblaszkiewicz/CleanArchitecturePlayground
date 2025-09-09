using CompanyManager.Domain.Repositories;

namespace CompanyManager.Domain.Services
{
    public class EmployeeRecordNumberService : IEmployeeRecordNumberService
    {
        private static readonly SemaphoreSlim _semaphore = new(1, 1);

        public async Task<TResult> GenerateNextNumberAndExecuteAsync<TResult>(IEmployeesRepository employeesRepository, Func<int, CancellationToken, Task<TResult>> operationToExecute, CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken);
            try
            {
                var lastEmployee = await employeesRepository.GetLastEmployee(cancellationToken);
                var nextNumber = GetNextRecordNumber(lastEmployee?.RecordNumber?.IntValue);

                var result = await operationToExecute(nextNumber, cancellationToken);

                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private int GetNextRecordNumber(int? lastRecordNumber)
        {
            return lastRecordNumber.HasValue ? lastRecordNumber.Value + 1 : 1;
        }
    }
}
