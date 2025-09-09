using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Repositories;
using CompanyManager.Domain.ValueObjects.Employee;

namespace CompanyManager.Infrastructure.Repositories
{
    public class EmployeesRepository : IEmployeesRepository
    {
        public Task<Employee> Get(EmployeeId id, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task Add(Employee employee, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task Update(Employee employee, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetLastEmployee(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}