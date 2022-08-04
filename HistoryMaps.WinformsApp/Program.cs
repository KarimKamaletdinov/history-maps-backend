﻿using HistoryMaps;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

ApplicationConfiguration.Initialize();

var config = JsonConvert.DeserializeObject<Dictionary<string, string>>(File.ReadAllText("appsettings.json"))
             ?? throw new NullReferenceException();

// serilog
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "{Message:lj}{NewLine}{Exception}")
    .CreateLogger();

// default service collection
var services = new ServiceCollection();
services.AddLogging(x => x.AddSerilog(dispose:true));

// autofac container builder
var builder = new ContainerBuilder();

// add default SC t autofac
builder.Populate(services);

// storage
builder.RegisterType<PostgresqlConnectionFactory>().WithParameter("connectionString", config["ConnectionString"])
    .AsImplementedInterfaces();
builder.RegisterType<RootFolderProvider>().WithParameter("rootFolder", config["RootFolder"])
    .AsImplementedInterfaces();
builder.RegisterType<GitRemoteUrlProvider>().WithParameter("url", config["GitRemoteUrl"])
    .AsImplementedInterfaces();

// repositories
builder.RegisterType<WorldBmpRepository>().AsImplementedInterfaces();
builder.RegisterType<EventRepository>().AsImplementedInterfaces();
builder.RegisterType<World3MfRepository>().AsImplementedInterfaces();

//services
builder.RegisterType<Create3DWorldSeparatelyCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<GetWorldQueryHandler>().AsImplementedInterfaces();
builder.RegisterType<SynchronizeWorldCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<LoadHistoryCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<ExecuteGitCommandService>().AsSelf();
builder.RegisterType<GitCloneCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<GitPullCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<GitCommitCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<GitPushCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<LoadGitRepoCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<CopyDataToWebAppCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<SaveChangesToGitRepoCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<CreateWebAppCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<LoadAddedHistoryCommandHandler>().AsImplementedInterfaces();
builder.RegisterType<CreateEventHandler>().AsImplementedInterfaces();
builder.RegisterType<DeleteEventHandler>().AsImplementedInterfaces();
builder.RegisterType<ModifyEventHandler>().AsImplementedInterfaces();
builder.RegisterType<GetAllEventsHandler>().AsImplementedInterfaces();
builder.RegisterType<GetWorldBitmapHandler>().AsImplementedInterfaces();

//presenter
builder.RegisterType<Presenter>().AsSelf();

var container = builder.Build();
var presenter = container.Resolve<Presenter>();
var form = new MainForm();
await presenter.Initialize(form);
Application.Run(form);
