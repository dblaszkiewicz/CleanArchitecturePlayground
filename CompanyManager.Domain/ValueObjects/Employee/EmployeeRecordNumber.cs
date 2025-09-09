
namespace CompanyManager.Domain.ValueObjects.Employee
{
    public record EmployeeRecordNumber
    {
        public string Value { get; }

        public EmployeeRecordNumber(int value)
        {
            if (value < 0 || value > 99999999)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Employee record number must be a non-negative integer with a maximum of 8 digits.");
            }

            Value = value.ToString("D8");
        }

        public int IntValue => int.Parse(Value);

        public static implicit operator string(EmployeeRecordNumber employeeRecordNumber)
            => employeeRecordNumber.Value;

        public static implicit operator EmployeeRecordNumber(int number)
            => new(number);
    }
}
