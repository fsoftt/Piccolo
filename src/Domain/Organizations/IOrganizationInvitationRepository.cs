namespace Domain.Organizations
{
    public interface IOrganizationInvitationRepository
    {
        Task AddAsync(OrganizationInvitation invitation, CancellationToken cancellationToken);
    }
}
