namespace HistoryMaps;

public class GetWorldCommandHandler : IQueryHandler<GetWorld, WorldDto>
{
    private readonly IWorldBmpRepository _repository;

    public GetWorldCommandHandler(IWorldBmpRepository repository)
    {
        _repository = repository;
    }

    public WorldDto Execute(GetWorld query)
    {
        return _repository.Get(query.WorldId).ToDto();
    }
}