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

public class GenerateEventIdHandler : IQueryHandler<GenerateEventId, int>
{
    private readonly IEventRepository _repository;

    public GenerateEventIdHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public Task<int> Execute(GenerateEventId query)
    {
        return _repository.GenerateId(query.Year);
    }
}

public class GetPreviousEventHandler : IQueryHandler<GetPreviousEvent, EventDto?>
{
    private readonly IEventRepository _repository;

    public GetPreviousEventHandler(IEventRepository repository)
    {
        _repository = repository;
    }

    public Task<EventDto?> Execute(GetPreviousEvent query)
    {
        return _repository.GetPrevious(query.Year, query.Id);
    }
}