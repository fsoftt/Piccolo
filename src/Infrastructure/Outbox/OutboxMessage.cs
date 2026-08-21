namespace Infrastructure.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; private set; }

        public DateTime OccurredAt { get; private set; }

        public string Type { get; private set; } = null!;

        public string Payload { get; private set; } = null!;

        public string? Error { get; private set; }

        public int Attempts { get; private set; }

        public bool Processed { get; private set; }

        public DateTime? ProcessedAt { get; private set; }

        private OutboxMessage()
        {
        }

        public OutboxMessage(string type, string payload)
        {
            Id = Guid.NewGuid();
            OccurredAt = DateTime.UtcNow;
            Type = type;
            Payload = payload;
            Attempts = 0;
            Processed = false;
        }

        public void MarkProcessed()
        {
            Processed = true;
            ProcessedAt = DateTime.UtcNow;
        }

        public void IncrementAttempts(string? error)
        {
            Attempts++;
            Error = error;
        }
    }
}
