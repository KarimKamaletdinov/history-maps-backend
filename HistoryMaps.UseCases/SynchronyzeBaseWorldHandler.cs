namespace HistoryMaps;

public class SynchronyzeBaseWorldHandler : ICommandHandler<SynchronizeBaseWorld>
{
    private readonly IQueryHandler<GetBaseWorld, WorldDto> _getWorld;
    private readonly ICommandHandler<Create3DBaseWorld> _create3DWorld;
    private readonly IRootFolderProvider _rootFolder;

    public SynchronyzeBaseWorldHandler(IQueryHandler<GetBaseWorld, WorldDto> getWorld,
        ICommandHandler<Create3DBaseWorld> create3DWorld, IRootFolderProvider rootFolder)
    {
        _getWorld = getWorld;
        _create3DWorld = create3DWorld;
        _rootFolder = rootFolder;
    }

    public void Execute(SynchronizeBaseWorld command)
    {
        if (Directory.Exists(_rootFolder.GetPath("worlds",
                "baseworld")))
            Directory.Delete(_rootFolder.GetPath("worlds",
                "baseworld"), true);
        var world = _getWorld.Execute(new ());
        _create3DWorld.Execute(new (world));
    }
}