namespace CompanyManager.Domain.ValueObjects.Employee
{
    public record EmployeeSurname
    {
        public string Value { get; }

        public EmployeeSurname(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Employee surname cannot be empty.");
            }

            if (value.Length > 50)
            {
                throw new ArgumentException("Employee surname cannot be longer than 50 characters.");
            }

            Value = value;
        }

        public static implicit operator string(EmployeeSurname surname)
            => surname.Value;

        public static implicit operator EmployeeSurname(string surname)
            => new(surname);
    }
}
