namespace Business.Users.GetCurrentUser
{
    public sealed record GetCurrentUserResponse(
        Guid UserId,
        string Email);
}
