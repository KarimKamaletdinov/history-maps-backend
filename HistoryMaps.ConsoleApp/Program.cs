using Autofac;
using HistoryMaps;
using Newtonsoft.Json;

var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("appsettings.json"))
             ?? throw new NullReferenceException();

var builder = new ContainerBuilder();

// storage
builder.RegisterType<PostgresqlConnectionFactory>().WithParameter("connectionString", config["ConnectionString"])
    .AsImplementedInterfaces();
builder.RegisterType<RootFolderProvider>().WithParameter("rootFolder", config["RootFolder"])
    .AsImplementedInterfaces();

// repositories
builder.RegisterType<WorldBmpRepository>().AsImplementedInterfaces();
builder.RegisterType<EventRepository>().AsImplementedInterfaces();
builder.RegisterType<ThreeMfRepository>().AsImplementedInterfaces();


//services
builder.RegisterType<Create3DWorldCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<GetWorldQueryHandler>().AsImplementedInterfaces();
builder.RegisterType<SynchronizeWorldCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<GenerateWorldsCommandHandler>().AsImplementedInterfaces();


// views
builder.RegisterType<LoadHistoryView>().AsSelf();
builder.RegisterType<AddEventView>().AsSelf();
builder.RegisterType<CreateAppView>().AsSelf();
builder.RegisterType<HelpView>().AsSelf();
builder.RegisterType<InvalidCommandView>().AsSelf();

// app
builder.RegisterType<Application>().AsSelf();

var container = builder.Build();
var app = container.Resolve<Application>();
app.Run();