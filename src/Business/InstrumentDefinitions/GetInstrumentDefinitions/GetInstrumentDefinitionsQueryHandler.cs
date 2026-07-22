using Domain.Common;
using Domain.Instruments;
using MediatR;

namespace Business.InstrumentDefinitions.GetInstrumentDefinitions
{
    public sealed class GetInstrumentDefinitionsQueryHandler
        : IRequestHandler<
            GetInstrumentDefinitionsQuery,
            Result<IReadOnlyList<InstrumentDefinitionResponse>>>
    {
        private readonly IInstrumentDefinitionRepository repository;

        public GetInstrumentDefinitionsQueryHandler(
            IInstrumentDefinitionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<IReadOnlyList<InstrumentDefinitionResponse>>> Handle(
            GetInstrumentDefinitionsQuery request,
            CancellationToken cancellationToken)
        {
            var definitions = await repository.ListAsync(cancellationToken);

            var response = definitions
                .Select(x => new InstrumentDefinitionResponse(
                    x.Id,
                    x.Name,
                    x.Family))
                .ToList();

            return Result<IReadOnlyList<InstrumentDefinitionResponse>>.Success(response);
        }
    }
}
