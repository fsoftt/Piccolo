namespace API.Common
{
    public sealed record ApiError(
        string Code,
        string Description);
}
