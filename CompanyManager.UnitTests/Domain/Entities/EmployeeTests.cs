using CompanyManager.Domain.Entities;
using CompanyManager.Domain.Enums;
using CompanyManager.Domain.ValueObjects.Employee;
using FluentAssertions;

namespace CompanyManager.Tests.Domain.Entities
{
    public class EmployeeTests
    {
        private readonly EmployeeRecordNumber _validRecordNumber = new(1);
        private readonly EmployeeSurname _validSurname = new("Kowalski");
        private readonly EmployeeGender _validGender = new(GenderEnum.Male);

        [Fact]
        public void Constructor_Should_CreateEmployee_When_AllParametersAreValid()
        {
            var employee = new Employee(_validRecordNumber, _validGender, _validSurname);

            employee.Should().NotBeNull();
            employee.Id.Should().NotBeNull();
            employee.Id.Value.Should().NotBe(Guid.Empty);
            employee.RecordNumber.Should().Be(_validRecordNumber);
            employee.Gender.Should().Be(_validGender);
            employee.Surname.Should().Be(_validSurname);
            employee.InsertDate.Should().BeCloseTo(DateTimeOffset.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public void Constructor_Should_ThrowArgumentNullException_When_RecordNumberIsNull()
        {
            Action act = () => new Employee(null, _validGender, _validSurname);

            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("recordNumber");
        }

        [Fact]
        public void Constructor_Should_ThrowArgumentNullException_When_GenderIsNull()
        {
            Action act = () => new Employee(_validRecordNumber, null, _validSurname);

            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("gender");
        }

        [Fact]
        public void Constructor_Should_ThrowArgumentNullException_When_SurnameIsNull()
        {
            Action act = () => new Employee(_validRecordNumber, _validGender, null);

            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("surname");
        }

        [Fact]
        public void UpdateSurname_Should_ChangeSurname_When_NewSurnameIsValid()
        {
            var employee = new Employee(_validRecordNumber, _validGender, _validSurname);
            var newSurname = new EmployeeSurname("Nowak");

            employee.UpdateSurname(newSurname);

            employee.Surname.Should().Be(newSurname);
        }

        [Fact]
        public void UpdateSurname_Should_ThrowArgumentNullException_When_NewSurnameIsNull()
        {
            var employee = new Employee(_validRecordNumber, _validGender, _validSurname);

            Action act = () => employee.UpdateSurname(null);

            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("surname");
        }
    }
}