namespace Business.Users.Queries.GetCurrentUser
{
    public sealed record GetCurrentUserResponse(
        Guid UserId,
        string Email);
}
