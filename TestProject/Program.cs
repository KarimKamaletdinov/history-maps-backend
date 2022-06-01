using System.Text.Json;
using HistoryMaps;
var config = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText("appsettings.json"))
    ?? throw new NullReferenceException();

Console.WriteLine("Start");
var rootFolder = new RootFolderProvider(config["RootFolder"]);
var bmpRepo = new WorldBmpRepository(rootFolder);
var eventRepo = new EventRepository(new PostgresqlConnectionFactory(config["ConnectionString"]), bmpRepo);
new GenerateWorldsCommandHandler(bmpRepo, eventRepo,
    new SynchronizeWorldCommandHandler(new GetWorldQueryHandler(bmpRepo),
        new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)).Execute(new GenerateWorlds());
Console.WriteLine("History loaded");

Console.WriteLine("Enter year. You can specify ID by adding a '/' as a separator: '<year>/<id>':");
var line = Console.ReadLine();
if (line == null)
    throw new NullReferenceException();
var year = int.Parse(line.Contains('/') 
    ? line.Split('/')[0]
    : line);

int? id = line.Contains('/')
    ? int.Parse(line.Split('/')[1])
    : null;

Console.WriteLine("Enter name:");
var name = Console.ReadLine() ?? "";

var previous = eventRepo.GetPrevious(year, id);

var worldId = Guid.NewGuid();
File.Copy(rootFolder.GetPath("input", ".bmp"), 
    rootFolder.GetPath("worlds", worldId + ".bmp"));
File.Copy(rootFolder.GetPath("input", ".json"), 
    rootFolder.GetPath("worlds", worldId + ".json"));
Console.WriteLine($"File copied. Id: {worldId}");
var bw = previous == null ? bmpRepo.GetBaseWorld() : bmpRepo.Get(previous.WorldId);
var nw = bmpRepo.Get(worldId);
Console.WriteLine("Worlds loaded");
var changes = Event.ParseChanges(bw, nw);
Console.WriteLine("Changes parsed");
var e = new Event(year, name, changes.ToArray(), bw, nw.Id);
eventRepo
    .Insert(e);
Console.WriteLine("Changes inserted");
new GenerateWorldsCommandHandler(bmpRepo, eventRepo,
    new SynchronizeWorldCommandHandler(new GetWorldQueryHandler(bmpRepo),
        new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)).Execute(new GenerateWorlds());

File.WriteAllText(rootFolder.GetPath("events.json"), JsonSerializer.Serialize(eventRepo.GetAllEvents().Select(x => x.ToDto()), new JsonSerializerOptions()
{
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
}));