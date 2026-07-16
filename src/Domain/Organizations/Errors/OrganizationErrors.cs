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
    }
}
