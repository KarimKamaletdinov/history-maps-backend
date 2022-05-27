using HistoryMaps;

Console.WriteLine(Guid.Empty);
var rootFolder = new RootFolderProvider();

new SynchronizeWorldCommandHandler(new GetWorldCommandHandler(new WorldBmpRepository(rootFolder)),
    new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)
    .Execute(new SynchronizeWorld(Guid.Parse("00000000-0000-0000-0000-000000000000")));