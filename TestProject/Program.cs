using System.Text.Json;
using HistoryMaps;


Console.WriteLine("Start");
var rootFolder = new RootFolderProvider();
var bmpRepo = new WorldBmpRepository(rootFolder);
var eventRepo = new EventRepository(new PostgresqlConnectionFactory(), bmpRepo);
new GenerateWorldsCommandHandler(bmpRepo, eventRepo,
    new SynchronizeWorldCommandHandler(new GetWorldQueryHandler(bmpRepo),
        new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)).Execute(new GenerateWorlds());
Console.WriteLine("History loaded");
    

Console.WriteLine("Enter year:");
var year = int.Parse(Console.ReadLine() ?? "");

Console.WriteLine("Enter name:");
var name = Console.ReadLine() ?? "";

var previous = eventRepo.GetAllEvents().Last();

var id = Guid.NewGuid();
File.Copy(rootFolder.GetPath("input", ".bmp"), 
    rootFolder.GetPath("worlds", id + ".bmp"));
File.Copy(rootFolder.GetPath("input", ".json"), 
    rootFolder.GetPath("worlds", id + ".json"));
Console.WriteLine($"File copied. Id: {id}");
var bw = bmpRepo.Get(previous.ToDto().WorldId);
var nw = bmpRepo.Get(id);
Console.WriteLine("Worlds loaded");
var changes = Event.ParseChanges(bw, nw);
Console.WriteLine("Changes parsed");
var e = new Event(Guid.NewGuid(), name,
    year, changes.ToArray(), bw, nw.Id);
eventRepo
    .Insert(e, previous.Id);
Console.WriteLine("Changes inserted");
new GenerateWorldsCommandHandler(bmpRepo, eventRepo,
    new SynchronizeWorldCommandHandler(new GetWorldQueryHandler(bmpRepo),
        new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)).Execute(new GenerateWorlds());

File.WriteAllText(rootFolder.GetPath("events.json"), JsonSerializer.Serialize(eventRepo.GetAllEvents().Select(x => x.ToDto()), new JsonSerializerOptions()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
}));