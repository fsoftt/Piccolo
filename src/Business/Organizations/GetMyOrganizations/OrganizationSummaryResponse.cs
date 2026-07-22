namespace Business.Organizations.GetMyOrganizations
{
    public sealed record OrganizationSummaryResponse(
        Guid Id,
        string Name);
}
