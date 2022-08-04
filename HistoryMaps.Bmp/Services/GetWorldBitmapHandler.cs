namespace HistoryMaps;

public class GetWorldBitmapHandler : IQueryHandler<GetWorldBitmap, WorldBitmapDto>
{
    private readonly IWorldBmpRepository _repository;

    public GetWorldBitmapHandler(IWorldBmpRepository repository)
    {
        _repository = repository;
    }

    public WorldBitmapDto Execute(GetWorldBitmap query)
    {
        return _repository.GetBitmap(query.WorldId);
    }
}