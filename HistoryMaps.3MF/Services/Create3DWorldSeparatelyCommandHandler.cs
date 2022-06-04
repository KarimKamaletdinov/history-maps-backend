namespace HistoryMaps;

public class Create3DWorldSeparatelyCommandHandler : ICommandHandler<Create3DWorldSeparately>
{
    private readonly IWorld3MfRepository _repository;

    public Create3DWorldSeparatelyCommandHandler(IWorld3MfRepository repository)
    {
        _repository = repository;
    }

    public void Execute(Create3DWorldSeparately command)
    {
        _repository.InsertSeparately(command.World);
    }
}