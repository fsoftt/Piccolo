using FluentValidation;

namespace Business.Organizations.CreateOrganization
{
    public sealed class CreateOrganizationValidator
        : AbstractValidator<CreateOrganizationCommand>
    {
        public CreateOrganizationValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(150);
        }
    }
}
