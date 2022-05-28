using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HistoryMaps;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddApplicationPart(Assembly.Load("HistoryMaps.Controllers"));
builder.Services.AddSwaggerGen();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(Configure);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();

var exeFile = new Uri(Assembly.GetEntryAssembly().CodeBase).AbsolutePath;
var dir = Path.GetDirectoryName(exeFile);
var rootPath = Path.GetFullPath(Path.Combine(dir, builder.Configuration["RootFolder"]));
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
    b.RegisterType<RootFolderProvider>().WithParameter("rootFolder",builder.Configuration["RootFolder"]).AsImplementedInterfaces();

    //repositories
    b.RegisterType<WorldBmpRepository>().AsImplementedInterfaces();
    b.RegisterType<EventRepository>().AsImplementedInterfaces();

    //services
    b.RegisterType<GetAllEventsQueryHandler>().AsImplementedInterfaces();
}