namespace HistoryMaps;

public class GetWorldQueryHandler : IQueryHandler<GetWorld, WorldDto>
{
    private readonly IWorldBmpRepository _repository;

    public GetWorldQueryHandler(IWorldBmpRepository repository)
    {
        _repository = repository;
    }

    public WorldDto Execute(GetWorld query)
    {
        return _repository.Get(query.WorldId).ToDto();
    }
}