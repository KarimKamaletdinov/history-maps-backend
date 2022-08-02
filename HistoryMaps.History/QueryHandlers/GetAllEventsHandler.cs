namespace HistoryMaps;

public class GetAllEventsHandler : IQueryHandler<GetAllEvents, IEnumerable<EventDto>>
{
    private readonly IEventRepository _repository;

    public GetAllEventsHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EventDto>> Execute(GetAllEvents query)
    {
        return await _repository.GetAllEventDtos();
    }
}