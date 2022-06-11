namespace HistoryMaps;

public class GetAllEventsQueryHandler : IQueryHandler<GetAllEvents, IEnumerable<EventChangesDto>>
{
    private readonly IEventRepository _repository;

    public GetAllEventsQueryHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<EventChangesDto> Execute(GetAllEvents query)
    {
        return _repository.GetAllEvents().Select(x => x.ToDto());
    }
}