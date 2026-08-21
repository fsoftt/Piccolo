namespace Domain.Organizations
{
    public interface IOrganizationInvitationRepository
    {
        Task AddAsync(OrganizationInvitation invitation, CancellationToken cancellationToken);
        Task<IReadOnlyList<OrganizationInvitation>> ListPendingAsync(CancellationToken cancellationToken);
        Task UpdateAsync(OrganizationInvitation invitation, CancellationToken cancellationToken);
    }
}
