using CompanyManager.Application.Commands.Employees;
using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Enums;
using CompanyManager.Domain.Repositories;
using CompanyManager.Domain.Services;
using FluentAssertions;
using Moq;

namespace CompanyManager.UnitTests.Application.Commands
{
    public class CreateEmployeeCommandHandlerTests
    {
        private readonly Mock<IEmployeesRepository> _employeeRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<IEmployeeRecordNumberService> _recordNumberServiceMock;
        private readonly CreateEmployeeCommandHandler _handler;

        public CreateEmployeeCommandHandlerTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeesRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _recordNumberServiceMock = new Mock<IEmployeeRecordNumberService>();

            _handler = new CreateEmployeeCommandHandler(
                _employeeRepositoryMock.Object,
                _unitOfWorkMock.Object,
                _recordNumberServiceMock.Object);
        }

        [Fact]
        public async Task Handle_Should_CallAddAndCommit_WhenCommandIsValid()
        {
            var command = new CreateEmployeeCommand { Surname = "Nowak", Gender = GenderEnum.Female };
            var expectedRecordNumber = 123;
            Employee createdEmployee = null;

            _employeeRepositoryMock
                .Setup(repo => repo.Add(It.IsAny<Employee>(), It.IsAny<CancellationToken>()))
                .Callback<Employee, CancellationToken>((emp, ct) => createdEmployee = emp);

            _recordNumberServiceMock
                .Setup(s => s.GenerateNextNumberAndExecuteAsync(
                    _employeeRepositoryMock.Object,
                    It.IsAny<Func<int, CancellationToken, Task<Guid>>>(),
                    It.IsAny<CancellationToken>()))
                .Returns<IEmployeesRepository, Func<int, CancellationToken, Task<Guid>>, CancellationToken>(
                    async (repo, operation, token) => await operation(expectedRecordNumber, token));

            var resultGuid = await _handler.Handle(command, CancellationToken.None);

            resultGuid.Should().NotBe(Guid.Empty);
            resultGuid.Should().Be(createdEmployee.Id.Value);

            createdEmployee.Should().NotBeNull();
            createdEmployee.Surname.Value.Should().Be(command.Surname);
            createdEmployee.Gender.Value.Should().Be(command.Gender);
            createdEmployee.RecordNumber.IntValue.Should().Be(expectedRecordNumber);

            _employeeRepositoryMock.Verify(
                repo => repo.Add(It.IsAny<Employee>(), It.IsAny<CancellationToken>()),
                Times.Once);

            _unitOfWorkMock.Verify(
                uow => uow.CommitAsync(It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
