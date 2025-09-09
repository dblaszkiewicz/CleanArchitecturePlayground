using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Enums;
using CompanyManager.Domain.Repositories;
using CompanyManager.Domain.Services;
using MediatR;

namespace CompanyManager.Application.Commands.Employees
{
    public record CreateEmployeeCommand : IRequest<Guid>
    {
        public required string Surname { get; set; }

        public required GenderEnum Gender { get; set; }
    }

    public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, Guid>
    {
        private readonly IEmployeesRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmployeeRecordNumberService _employeeRecordNumberService;

        public CreateEmployeeCommandHandler(
            IEmployeesRepository employeeRepository,
            IUnitOfWork unitOfWork,
            IEmployeeRecordNumberService employeeRecordNumberService)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _employeeRecordNumberService = employeeRecordNumberService;
        }

        public async Task<Guid> Handle(CreateEmployeeCommand request, CancellationToken ct)
        {
            ArgumentNullException.ThrowIfNull(request);

            var newEmployeeId = await _employeeRecordNumberService.GenerateNextNumberAndExecuteAsync(
                _employeeRepository,
                async (nextRecordNumber, cancellationToken) =>
                {
                    var newEmployee = new Employee(nextRecordNumber, request.Gender, request.Surname);

                    await _employeeRepository.Add(newEmployee, cancellationToken);
                    await _unitOfWork.CommitAsync(cancellationToken);

                    return newEmployee.Id.Value;
                },
                ct);

            return newEmployeeId;
        }
    }
}