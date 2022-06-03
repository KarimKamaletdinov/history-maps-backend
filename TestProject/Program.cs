using System.Text;
using HistoryMaps;
using Newtonsoft.Json;

var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("appsettings.json"))
             ?? throw new NullReferenceException();


Console.WriteLine("Start");
var rootFolder = new RootFolderProvider(config["RootFolder"]);
var bmpRepo = new WorldBmpRepository(rootFolder);
var eventRepo = new EventRepository(new PostgresqlConnectionFactory(config["ConnectionString"]), bmpRepo);

while (true)
{
    Console.WriteLine("Type a command ('l' or 'load' for loading history, 'a' or 'add' for adding event," +
                      "'c' or 'create' for creating a client app (NOT IMPLEMENTED)");
    var cmd = Console.ReadLine();

    switch (cmd)
    {
        case "l":
        case "load":
            Console.WriteLine("Start loading history...");
            bmpRepo.ClearAll();
            Console.WriteLine("Cleared");
            foreach (var ev in eventRepo.GetAllEvents())
            {
                bmpRepo.Insert(ev.World);
                Console.WriteLine("Loaded: " + ev.Name);
            }
            Console.WriteLine("Finished");
            break;

        case "a":
        case "add":
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
            eventRepo.Insert(e);
            Console.WriteLine("Changes inserted");
            Console.WriteLine("Finished");
            break;

        case "c":
        case "create":
            //Console.WriteLine("Start creating a web app");
            Console.WriteLine("Creating a web app is not implemented yet");
            new GenerateWorldsCommandHandler(bmpRepo, eventRepo,
                new SynchronizeWorldCommandHandler(new GetWorldQueryHandler(bmpRepo),
                    new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)),
                    rootFolder)).Execute(new ());
            Console.WriteLine("Loaded history and generated .3MF files");
            File.WriteAllText(rootFolder.GetPath("events.json"), JsonConvert.SerializeObject(eventRepo.GetAllEventDtos()),
                Encoding.UTF8);
            Console.WriteLine("Events metadata saved");
            Console.WriteLine("Finished");
            break;
    }
}


