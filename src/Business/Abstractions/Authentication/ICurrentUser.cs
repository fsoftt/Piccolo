namespace Business.Abstractions.Authentication
{
    public interface ICurrentUser
    {
        Guid? UserId { get; }
        string? Email { get; }
    }
}
