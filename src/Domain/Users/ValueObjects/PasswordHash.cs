using Domain.Common;
using Domain.Users.Errors;

namespace Domain.Users.ValueObjects
{
    public sealed class PasswordHash : ValueObject
    {
        private PasswordHash(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<PasswordHash> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<PasswordHash>.Failure(
                    PasswordHashErrors.Empty);
            }

            value = value.Trim();

            return Result<PasswordHash>.Success(
                new PasswordHash(value));
        }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
            => Value;

        public static implicit operator string(
            PasswordHash passwordHash)
            => passwordHash.Value;
    }
}
