namespace Business.Authentication
{
    public interface ICurrentUser
    {
        bool IsAuthenticated { get; }
        Guid? UserId { get; }
        string? Email { get; }
    }
}
