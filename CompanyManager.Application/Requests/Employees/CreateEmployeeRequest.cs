
using CompanyManager.Domain.Enums;

namespace CompanyManager.Application.Requests.Employees
{
    public record CreateEmployeeRequest(string Surname, GenderEnum Gender);
}
