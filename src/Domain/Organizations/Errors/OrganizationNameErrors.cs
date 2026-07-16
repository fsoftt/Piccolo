using Domain.Common;

namespace Domain.Organizations.Errors
{
    public static class OrganizationNameErrors
    {
        public static readonly Error Empty =
        new(
            "Organization.Name.Empty",
            "Organization name cannot be empty.");

        public static readonly Error TooLong =
            new(
                "Organization.Name.TooLong",
                "Organization name cannot exceed 150 characters.");
    }
}
