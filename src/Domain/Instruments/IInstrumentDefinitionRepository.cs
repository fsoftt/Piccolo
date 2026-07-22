namespace Domain.Instruments
{
    public interface IInstrumentDefinitionRepository
    {
        Task<IReadOnlyList<InstrumentDefinition>> ListAsync(
            CancellationToken cancellationToken);
    }
}
