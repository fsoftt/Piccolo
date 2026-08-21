namespace Business.Abstractions.Persistence
{
    public interface IOutboxRepository
    {
        Task AddAsync(string type, string payload, CancellationToken cancellationToken);
    }
}
