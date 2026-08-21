namespace Business.Organizations.Invitations.CreateInvitation
{
    public sealed record CreateOrganizationInvitationResponse(
        Guid Id,
        Guid OrganizationId,
        string Email,
        DateTime ExpiresAt,
        string Token);
}
