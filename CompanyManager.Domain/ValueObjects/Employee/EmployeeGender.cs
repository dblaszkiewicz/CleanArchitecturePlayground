using CompanyManager.Domain.Enums;

namespace CompanyManager.Domain.ValueObjects.Employee
{
    public record EmployeeGender
    {
        public GenderEnum Value { get; }

        public EmployeeGender(GenderEnum value)
        {
            Value = value;
        }

        public static implicit operator GenderEnum(EmployeeGender id)
            => id.Value;

        public static implicit operator EmployeeGender(GenderEnum id)
            => new(id);
    }
}