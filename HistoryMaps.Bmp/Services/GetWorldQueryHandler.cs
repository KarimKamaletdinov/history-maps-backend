namespace HistoryMaps;

public class GetWorldQueryHandler : IQueryHandler<GetWorld, WorldDto>
{
    private readonly IWorldBmpRepository _repository;

    public GetWorldQueryHandler(IWorldBmpRepository repository)
    {
        _repository = repository;
    }

    public async Task<WorldDto> Execute(GetWorld query)
    {
        return (await _repository.Get(query.WorldId)).ToDto();
    }
}