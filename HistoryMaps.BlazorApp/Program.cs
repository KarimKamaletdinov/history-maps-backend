using Autofac;
using Autofac.Extensions.DependencyInjection;
using HistoryMaps;
using MudBlazor.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Host.UseServiceProviderFactory(
    new AutofacServiceProviderFactory(ConfigureServices));

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();


void ConfigureServices(ContainerBuilder b)
{
    // storage
    b.RegisterType<PostgresqlConnectionFactory>().WithParameter("connectionString", builder.Configuration["ConnectionString"])
        .AsImplementedInterfaces();
    b.RegisterType<RootFolderProvider>().WithParameter("rootFolder", builder.Configuration["RootFolder"])
        .AsImplementedInterfaces();
    b.RegisterType<GitRemoteUrlProvider>().WithParameter("url", builder.Configuration["GitRemoteUrl"])
        .AsImplementedInterfaces();

    // repositories
    b.RegisterType<WorldBmpRepository>().AsImplementedInterfaces();
    b.RegisterType<EventRepository>().AsImplementedInterfaces();
    b.RegisterType<World3MfRepository>().AsImplementedInterfaces();

    // services
    b.RegisterType<Create3DWorldCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<Create3DWorldSeparatelyCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<GetWorldQueryHandler>().AsImplementedInterfaces();
    b.RegisterType<SynchronizeWorldCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<LoadHistoryCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<ExecuteGitCommandService>().AsSelf();
    b.RegisterType<GitCloneCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<GitPullCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<GitCommitCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<GitPushCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<LoadGitRepoCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<CopyDataToWebAppCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<SaveChangesToGitRepoCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<CreateWebAppCommandHandler>().AsImplementedInterfaces();
    b.RegisterType<GetAllEventsQueryHandler>().AsImplementedInterfaces();
    b.RegisterType<LoadAddedHistoryCommandHandler>().AsImplementedInterfaces();
}
