namespace Domain.Instruments
{
    public sealed class InstrumentDefinition
    {
        private InstrumentDefinition()
        {
        }

        public InstrumentDefinition(
            Guid id,
            string name,
            InstrumentFamily family)
        {
            Id = id;
            Name = name;
            Family = family;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public InstrumentFamily Family { get; private set; }
    }
}
