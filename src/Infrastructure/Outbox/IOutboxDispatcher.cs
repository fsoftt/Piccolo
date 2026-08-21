namespace Infrastructure.Outbox
{
    public interface IOutboxDispatcher
    {
        Task DispatchAsync(
            OutboxMessage message,
            IServiceProvider serviceProvider,
            CancellationToken cancellationToken);
    }
}
