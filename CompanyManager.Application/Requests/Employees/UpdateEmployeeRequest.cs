using CompanyManager.Domain.Enums;

namespace CompanyManager.Application.Requests.Employees
{
    public record UpdateEmployeeRequest(string Surname, GenderEnum Gender);
}
