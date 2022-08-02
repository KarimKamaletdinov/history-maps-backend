namespace HistoryMaps;

public class GetAllEventsWithChangesHandler : IQueryHandler<GetAllEventsWithChanges, IEnumerable<EventChangesDto>>
{
    private readonly IEventRepository _repository;

    public GetAllEventsWithChangesHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EventChangesDto>> Execute(GetAllEventsWithChanges query)
    {
        return (await _repository.GetAllEvents()).Select(x => x.ToDtoWithChanges());
    }
}