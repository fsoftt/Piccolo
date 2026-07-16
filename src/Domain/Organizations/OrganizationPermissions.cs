namespace Domain.Organizations
{
    public static class OrganizationPermissions
    {
        public static IReadOnlyCollection<OrganizationPermission> Owner =>
        [
            OrganizationPermission.ManageOrganization,
            OrganizationPermission.ManageMembers,
            OrganizationPermission.InviteMembers,
            OrganizationPermission.UploadWorks,
            OrganizationPermission.AssignParts,
            OrganizationPermission.ManageFolders
        ];
    }
}
