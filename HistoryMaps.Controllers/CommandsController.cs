using Microsoft.AspNetCore.Mvc;

namespace HistoryMaps;

[Route("[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    private readonly ICommandHandler<GenerateWorlds> _generateWorlds;

    public CommandsController(ICommandHandler<GenerateWorlds> generateWorlds)
    {
        _generateWorlds = generateWorlds;
    }

    [HttpPost]
    public IActionResult Post(CommandType type)
    {
        switch (type)
        {
            case CommandType.GenerateWorlds:
                _generateWorlds.Execute(new GenerateWorlds());
                return new CreatedResult("/world/", null);
            default:
                return new BadRequestResult();
        }
    }
}