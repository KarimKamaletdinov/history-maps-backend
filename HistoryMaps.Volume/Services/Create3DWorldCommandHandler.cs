namespace HistoryMaps;

public class Create3DWorldCommandHandler : ICommandHandler<Create3DWorld>
{
    private readonly IVolumeWorldRepository _repository;

    public Create3DWorldCommandHandler(IVolumeWorldRepository repository)
    {
        _repository = repository;
    }

    public void Execute(Create3DWorld command)
    {
        _repository.Insert(command.World);
    }
}