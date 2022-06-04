namespace HistoryMaps;

public class GetAllEventsQueryHandler : IQueryHandler<GetAllEvents, IEnumerable<EventDto>>
{
    private readonly IEventRepository _repository;

    public GetAllEventsQueryHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<EventDto> Execute(GetAllEvents query)
    {
        return _repository.GetAllEvents().Select(x => x.ToDto());
    }
}