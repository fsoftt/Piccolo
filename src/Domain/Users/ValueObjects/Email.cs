using Domain.Common;
using Domain.Users.Errors;
using System.Text.RegularExpressions;

namespace Domain.Users.ValueObjects
{
    public class Email : ValueObject
    {
        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$", 
            RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private Email(string value)
        {
            Value = value;
        }

        public string Value { get; }

        public static Result<Email> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Result<Email>.Failure(EmailErrors.Empty);
            }

            value = value.Trim().ToLowerInvariant();

            if (!EmailRegex.IsMatch(value))
            {
                return Result<Email>.Failure(EmailErrors.InvalidFormat);
            }

            return Result<Email>.Success(new Email(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value;

        public static implicit operator string(Email email) => email.Value;
    }
}
