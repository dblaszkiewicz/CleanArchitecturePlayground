using CompanyManager.Domain.Enums;
using CompanyManager.Domain.Repositories;
using MediatR;

namespace CompanyManager.Application.Commands.Employees
{
    public record UpdateEmployeeCommand : IRequest
    {
        public required Guid Id { get; set; }

        public required string Surname { get; set; }

        public required GenderEnum Gender { get; set; }
    }

    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEmployeeCommandHandler(
            IEmployeesRepository employeeRepository, 
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateEmployeeCommand request, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var employee = await _employeeRepository.Get(request.Id, ct);
            if (employee is null)
            {
                throw new InvalidOperationException($"Employee with Id {request.Id} not found.");
            }

            employee.UpdateSurname(request.Surname);
            employee.UpdateGender(request.Gender);

            await _employeeRepository.Update(employee, ct);

            await _unitOfWork.CommitAsync(ct);
        }
    }
}