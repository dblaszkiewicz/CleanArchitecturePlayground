using CompanyManager.Domain.ValueObjects.Employee;
using FluentAssertions;

namespace CompanyManager.Tests.Domain.ValueObjects
{
    public class EmployeeSurnameTests
    {
        [Fact]
        public void Constructor_Should_CreateSurname_When_ValueIsValid()
        {
            var validSurname = "Gęś";

            var surname = new EmployeeSurname(validSurname);

            surname.Should().NotBeNull();
            surname.Value.Should().Be(validSurname);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Constructor_Should_ThrowArgumentException_When_ValueIsEmptyOrWhitespace(string invalidSurname)
        {
            Action act = () => new EmployeeSurname(invalidSurname);

            act.Should().Throw<ArgumentException>()
               .WithMessage("Employee surname cannot be empty.");
        }

        [Fact]
        public void Constructor_Should_ThrowArgumentException_When_ValueIsLongerThan50Characters()
        {
            var longSurname = new string('a', 51);
            Action act = () => new EmployeeSurname(longSurname);

            act.Should().Throw<ArgumentException>()
               .WithMessage("Employee surname cannot be longer than 50 characters.");
        }

        [Fact]
        public void ImplicitConversion_ToString_Should_ReturnCorrectValue()
        {
            var surname = new EmployeeSurname("Test");

            string result = surname;

            result.Should().Be("Test");
        }

        [Fact]
        public void ImplicitConversion_FromString_Should_CreateCorrectObject()
        {
            var surnameString = "Test";

            EmployeeSurname surname = surnameString;

            surname.Value.Should().Be(surnameString);
        }
    }
}
