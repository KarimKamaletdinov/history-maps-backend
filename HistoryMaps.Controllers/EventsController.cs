using Microsoft.AspNetCore.Mvc;

namespace HistoryMaps;

[Route("[controller]")]
[ApiController]
public class EventsController : ControllerBase
{
    private readonly IQueryHandler<GetAllEvents, IEnumerable<EventDto>> _getAllEvents;

    public EventsController(IQueryHandler<GetAllEvents, IEnumerable<EventDto>> getAllEvents)
    {
        _getAllEvents = getAllEvents;
    }

    [HttpGet]
    public IEnumerable<EventDto> Get()
    {
        return _getAllEvents.Execute(new());
    }
}