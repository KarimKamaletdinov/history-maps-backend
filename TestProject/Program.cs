using HistoryMaps;

Console.WriteLine(Guid.Empty);
var rootFolder = new RootFolderProvider();

var bmpRepo = new WorldBmpRepository(rootFolder);
var events = new EventRepository(new PostgresqlConnectionFactory(), bmpRepo)
    .GetAllEvents();
foreach (var e in events)
{
   bmpRepo.Insert(e.World); 
   new SynchronizeWorldCommandHandler(new GetWorldCommandHandler(bmpRepo),
    new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)
    .Execute(new SynchronizeWorld(e.World.Id));
}
