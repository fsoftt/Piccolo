using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Outbox
{
    public sealed class OutboxProcessor : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<OutboxProcessor> logger;

        private readonly TimeSpan pollingInterval =
            TimeSpan.FromSeconds(5);

        public OutboxProcessor(
            IServiceProvider serviceProvider,
            ILogger<OutboxProcessor> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            logger.LogInformation("OutboxProcessor started");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await ProcessMessagesAsync(stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    logger.LogError(
                        ex,
                        "Error processing outbox messages");
                }

                try
                {
                    await Task.Delay(
                        pollingInterval,
                        stoppingToken);
                }
                catch (OperationCanceledException)
                    when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
            }

            logger.LogInformation("OutboxProcessor stopped");
        }

        private async Task ProcessMessagesAsync(
            CancellationToken cancellationToken)
        {
            using var scope =
                serviceProvider.CreateScope();

            var context =
                scope.ServiceProvider
                    .GetRequiredService<ApplicationDbContext>();

            var dispatcher =
                scope.ServiceProvider
                    .GetRequiredService<IOutboxDispatcher>();

            var messages =
                await context.Set<OutboxMessage>()
                    .Where(x => !x.Processed && x.Attempts < 3)
                    .OrderBy(x => x.OccurredAt)
                    .Take(20)
                    .ToListAsync(cancellationToken);

            foreach (var message in messages)
            {
                await ProcessMessageAsync(
                    message,
                    context,
                    dispatcher,
                    scope.ServiceProvider,
                    cancellationToken);
            }
        }

        private async Task ProcessMessageAsync(
            OutboxMessage message,
            ApplicationDbContext context,
            IOutboxDispatcher dispatcher,
            IServiceProvider serviceProvider,
            CancellationToken cancellationToken)
        {
            try
            {
                await dispatcher.DispatchAsync(
                    message,
                    serviceProvider,
                    cancellationToken);

                message.MarkProcessed();

                await context.SaveChangesAsync(
                    cancellationToken);

                logger.LogInformation(
                    "Outbox message {MessageId} processed successfully",
                    message.Id);
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Error processing outbox message {MessageId}",
                    message.Id);

                message.IncrementAttempts(
                    ex.Message);

                try
                {
                    await context.SaveChangesAsync(
                        cancellationToken);
                }
                catch (Exception saveException)
                {
                    logger.LogError(
                        saveException,
                        "Failed to save failure state for outbox message {MessageId}",
                        message.Id);
                }
            }
        }
    }
}
