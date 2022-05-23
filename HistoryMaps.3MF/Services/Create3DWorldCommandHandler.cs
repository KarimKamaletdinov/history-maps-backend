namespace HistoryMaps;

public class Create3DWorldCommandHandler : ICommandHandler<Create3DWorld>
{
    private readonly IThreeMfRepository _repository;

    public Create3DWorldCommandHandler(IThreeMfRepository repository)
    {
        _repository = repository;
    }

    public void Execute(Create3DWorld command)
    {
        var (doc, id) = To3MfConverter.Convert(command.World);
        _repository.Insert(doc, id);
    }
}