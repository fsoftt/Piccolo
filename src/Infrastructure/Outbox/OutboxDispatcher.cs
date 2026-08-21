using Domain.Organizations.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace Infrastructure.Outbox
{
    public sealed class OutboxDispatcher : IOutboxDispatcher
    {
        public async Task DispatchAsync(
            OutboxMessage message,
            IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            if (message.Type.Contains(typeof(InvitationCreatedDomainEvent).FullName!))
            {
                var domainEvent =
                    JsonSerializer.Deserialize<InvitationCreatedDomainEvent>(
                        message.Payload);
                if (domainEvent is null)
                {
                    throw new InvalidOperationException(
                        $"Could not deserialize outbox message {message.Id}.");
                }

                var handler =
                    serviceProvider.GetRequiredService<InvitationCreatedHandler>();

                await handler.HandleAsync(
                    domainEvent,
                    cancellationToken);

                return;
            }

            throw new InvalidOperationException(
                $"No handler registered for outbox event type '{message.Type}'.");
        }
    }
}
