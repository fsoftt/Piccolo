using Domain.Common;

namespace Domain.Organizations.Errors
{
    public static class OrganizationErrors
    {
        public static readonly Error DuplicateMember =
            new(
                "Organization.DuplicateMember",
                "The user already belongs to the organization.");

        public static readonly Error OwnerAlreadyExists =
            new(
                "Organization.OwnerAlreadyExists",
                "The organization already has an owner.");

        public static readonly Error DuplicateInstrumentName =
            new(
                "Organization.DuplicateInstrumentName",
                "An organization cannot contain duplicate instrument names.");

        public static readonly Error NotFound =
            new(
                "Organization.NotFound",
                "The organization was not found.");

        public static readonly Error AtLeastOneInstrumentRequired =
            new(
                "Organization.AtLeastOneInstrumentRequired",
                "An organization must have at least one instrument.");
    }
}
