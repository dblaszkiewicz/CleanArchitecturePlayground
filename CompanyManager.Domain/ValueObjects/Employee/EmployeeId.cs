
namespace CompanyManager.Domain.ValueObjects.Employee
{
    public record EmployeeId
    {
        public Guid Value { get; init; }

        public EmployeeId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("EmployeeId cannot be empty.");
            }

            Value = value;
        }

        public static EmployeeId Create()
        {
            var newGuid = Guid.NewGuid();
            return new EmployeeId(newGuid);
        }

        public static implicit operator Guid(EmployeeId id)
            => id.Value;

        public static implicit operator EmployeeId(Guid id)
            => new(id);
    }
}