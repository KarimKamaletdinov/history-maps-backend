namespace HistoryMaps;

public class Create3DWorldCommandHandler : ICommandHandler<Create3DWorld>
{
    private readonly IWorld3MfRepository _repository;

    public Create3DWorldCommandHandler(IWorld3MfRepository repository)
    {
        _repository = repository;
    }

    public void Execute(Create3DWorld command)
    {
        _repository.Insert(command.World);
    }
}