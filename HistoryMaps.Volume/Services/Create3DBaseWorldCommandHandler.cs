namespace HistoryMaps;

public class Create3DBaseWorldCommandHandler : ICommandHandler<Create3DBaseWorld>
{
    private readonly IVolumeWorldRepository _repository;

    public Create3DBaseWorldCommandHandler(IVolumeWorldRepository repository)
    {
        _repository = repository;
    }

    public void Execute(Create3DBaseWorld command)
    {
        _repository.InsertBaseWorld(command.BaseWorld);
    }
}