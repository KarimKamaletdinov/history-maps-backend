namespace HistoryMaps;

public class SynchronizeWorldCommandHandler : ICommandHandler<SynchronizeWorld>
{
    private readonly IQueryHandler<GetWorld, WorldDto> _getWorld;
    private readonly ICommandHandler<Create3DWorld> _create3DWorld;
    private readonly IRootFolderProvider _rootFolder;

    public SynchronizeWorldCommandHandler(IQueryHandler<GetWorld, WorldDto> getWorld,
        ICommandHandler<Create3DWorld> create3DWorld, IRootFolderProvider rootFolder)
    {
        _getWorld = getWorld;
        _create3DWorld = create3DWorld;
        _rootFolder = rootFolder;
    }

    public void Execute(SynchronizeWorld command)
    {
        if (Directory.Exists(_rootFolder.GetPath("worlds",
                command.WorldId.ToString())))
            Directory.Delete(_rootFolder.GetPath("worlds",
                command.WorldId.ToString()), true);
        var world = _getWorld.Execute(new (command.WorldId));
        _create3DWorld.Execute(new (world));
    }
}