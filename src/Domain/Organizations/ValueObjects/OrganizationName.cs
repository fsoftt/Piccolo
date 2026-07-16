using Domain.Common;
using Domain.Organizations.Errors;

namespace Domain.Organizations.ValueObjects
{
    public class OrganizationName : ValueObject
    {
        private const int MaxLength = 150;

        private OrganizationName(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<OrganizationName> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<OrganizationName>.Failure(
                    OrganizationNameErrors.Empty);
            }

            value = value.Trim();

            if (value.Length > MaxLength)
            {
                return Result<OrganizationName>.Failure(
                    OrganizationNameErrors.TooLong);
            }

            return Result<OrganizationName>.Success(
                new OrganizationName(value));
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value.ToUpperInvariant();
        }

        public override string ToString()
            => Value;

        public static implicit operator string(
            OrganizationName organizationName)
            => organizationName.Value;
    }
}
