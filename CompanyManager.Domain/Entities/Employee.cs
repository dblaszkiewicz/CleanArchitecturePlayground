using CompanyManager.Domain.ValueObjects.Employee;

namespace CompanyManager.Domain.Entities
{
    public class Employee
    {
        public EmployeeId Id { get; private set; }

        public EmployeeRecordNumber RecordNumber { get; private set; }

        public EmployeeGender Gender { get; private set; }

        public EmployeeSurname Surname { get; private set; }

        public DateTimeOffset InsertDate { get; private set; }

        public Employee(EmployeeRecordNumber recordNumber, EmployeeGender gender, EmployeeSurname surname)
        {
            ArgumentNullException.ThrowIfNull(recordNumber);
            ArgumentNullException.ThrowIfNull(gender);
            ArgumentNullException.ThrowIfNull(surname);

            Id = EmployeeId.Create();
            RecordNumber = recordNumber;
            Gender = gender;
            Surname = surname;
            InsertDate = DateTimeOffset.UtcNow;
        }

        public void UpdateSurname(EmployeeSurname surname)
        {
            ArgumentNullException.ThrowIfNull(surname);
            Surname = surname;
        }

        public void UpdateGender(EmployeeGender gender)
        {
            ArgumentNullException.ThrowIfNull(gender);
            Gender = gender;
        }
    }
}
