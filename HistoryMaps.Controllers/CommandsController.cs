using Microsoft.AspNetCore.Mvc;

namespace HistoryMaps;

[Route("[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandHandler<LoadHistory> _loadHistory;

    public CommandsController(ICommandHandler<LoadHistory> loadHistory)
    {
        _loadHistory = loadHistory;
    }

    [HttpPost]
    public IActionResult Post(CommandType type)
    {
        switch (type)
        {
            case CommandType.LoadHistory:
                _loadHistory.Execute(new());
                return new CreatedResult("/world/", null);
            default:
                return new BadRequestResult();
        }
    }
}