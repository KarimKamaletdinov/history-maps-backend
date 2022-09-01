namespace HistoryMaps;

public class GetBaseWorldHandler : IQueryHandler<GetBaseWorld, WorldDto>
{
    private readonly IWorldBmpRepository _repository;

    public GetBaseWorldHandler(IWorldBmpRepository repository)
    {
        _repository = repository;
    }

    public WorldDto Execute(GetBaseWorld query)
    {
        return _repository.GetBaseWorld().ToDto();
    }
}