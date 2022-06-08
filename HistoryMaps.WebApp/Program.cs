using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HistoryMaps;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddApplicationPart(Assembly.Load("HistoryMaps.Controllers"));
builder.Services.AddSwaggerGen();

var exeFile = new Uri(Assembly.GetEntryAssembly().Location).AbsolutePath;
var dir = Path.GetDirectoryName(exeFile);
var rootPath = Path.GetFullPath(Path.Combine(dir, builder.Configuration["RootFolder"]));

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(Configure);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseStaticFiles(new StaticFileOptions
{
    RequestPath = "/data",
    FileProvider = new PhysicalFileProvider(rootPath)
});
app.Run();

void Configure(ContainerBuilder b)
{
    //factories
    b.RegisterType<PostgresqlConnectionFactory>().WithParameter("connectionString", 
        builder.Configuration["ConnectionString"]).AsImplementedInterfaces();
    b.RegisterType<RootFolderProvider>().WithParameter("rootFolder",rootPath).AsImplementedInterfaces();

    //repositories
    b.RegisterType<WorldBmpRepository>().AsImplementedInterfaces();
    b.RegisterType<EventRepository>().AsImplementedInterfaces();
    b.RegisterType<World3MfRepository>().AsImplementedInterfaces();

    //services
    b.RegisterType<GetAllEventsQueryHandler>().AsImplementedInterfaces();
    b.RegisterType<GetWorldQueryHandler>().AsImplementedInterfaces();
    b.RegisterType<Create3DWorldCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<SynchronizeWorldCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<LoadHistoryCommandHandler>().AsImplementedInterfaces();
}