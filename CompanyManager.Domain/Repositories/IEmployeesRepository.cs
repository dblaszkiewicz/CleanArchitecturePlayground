using CompanyManager.Domain.Entities;
using CompanyManager.Domain.ValueObjects.Employee;

namespace CompanyManager.Domain.Repositories
{
    public interface IEmployeesRepository
    {
        Task<Employee> Get(EmployeeId id, CancellationToken ct);

        Task Add(Employee employee, CancellationToken ct);

        Task Update(Employee employee, CancellationToken ct);

        Task<Employee> GetLastEmployee(CancellationToken ct);
    }
}