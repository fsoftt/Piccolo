using Domain.Common;

namespace Domain.Organizations.Errors
{
    public static class InvitationErrors
    {
        public static readonly Error NotFound =
            new(
                "Invitation.NotFound",
                "The invitation was not found.");

        public static readonly Error Expired =
            new(
                "Invitation.Expired",
                "The invitation has expired.");

        public static readonly Error AlreadyProcessed =
            new(
                "Invitation.AlreadyProcessed",
                "The invitation has already been processed.");
    }
}
