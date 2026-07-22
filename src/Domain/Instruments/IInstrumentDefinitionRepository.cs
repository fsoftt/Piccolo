namespace Domain.Instruments
{
    public interface IInstrumentDefinitionRepository
    {
        Task<IReadOnlyList<InstrumentDefinition>> ListAsync(
            CancellationToken cancellationToken);

        Task<bool> ExistsAsync(
            IEnumerable<Guid> ids,
            CancellationToken cancellationToken);
    }
}
