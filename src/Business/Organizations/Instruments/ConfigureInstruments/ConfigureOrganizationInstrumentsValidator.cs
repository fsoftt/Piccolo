using FluentValidation;

namespace Business.Organizations.Instruments.ConfigureInstruments
{
    public sealed class ConfigureOrganizationInstrumentsValidator
    : AbstractValidator<ConfigureOrganizationInstrumentsCommand>
    {
        public ConfigureOrganizationInstrumentsValidator()
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty();

            RuleFor(x => x.Instruments)
                .NotEmpty();
        }
    }
}
