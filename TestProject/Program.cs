using HistoryMaps;

Console.WriteLine(Guid.Empty);
var rootFolder = new RootFolderProvider();

var bmpRepo = new WorldBmpRepository(rootFolder);
var events = new EventRepository(new PostgresqlConnectionFactory(), bmpRepo)
    .GetAllEvents();
Console.WriteLine(events.ElementAt(0).World.Id);
//new SynchronizeWorldCommandHandler(new GetWorldCommandHandler(new WorldBmpRepository(rootFolder)),
//    new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)
//    .Execute(new SynchronizeWorld(Guid.Parse("00000000-0000-0000-0000-000000000000")));