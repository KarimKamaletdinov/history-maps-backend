namespace HistoryMaps;

public class SynchronizeWorldCommandHandler : ICommandHandler<SynchronizeWorld>
{
    private readonly IQueryHandler<GetWorld, WorldDto> _getWorld;
    private readonly ICommandHandler<Create3DWorldSeparately> _create3DWorld;
    private readonly IRootFolderProvider _rootFolder;

    public SynchronizeWorldCommandHandler(IQueryHandler<GetWorld, WorldDto> getWorld,
        ICommandHandler<Create3DWorldSeparately> create3DWorld, IRootFolderProvider rootFolder)
    {
        _getWorld = getWorld;
        _create3DWorld = create3DWorld;
        _rootFolder = rootFolder;
    }

    public async Task Execute(SynchronizeWorld command)
    {
        if (File.Exists(_rootFolder.GetPath("worlds",
                command.WorldId + ".3mf")))
            File.Delete(_rootFolder.GetPath("worlds",
                command.WorldId + ".3mf"));
        var world = await _getWorld.Execute(new (command.WorldId));
        await _create3DWorld.Execute(new (world));
    }
}