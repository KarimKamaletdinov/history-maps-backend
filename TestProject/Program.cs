using System.Drawing;
using HistoryMaps;


var folderProvider = new RootFolderProvider();

var world = new WorldBmpRepository(folderProvider).Get(Guid.Parse("96d68a4e-f97f-4823-89c9-b34f93020c5e"),
    new Dictionary<string, Color> {{"water", Color.FromArgb(0, 162, 232)}});

if (File.Exists(folderProvider.GetPath("worlds",
        world.Id + ".3mf")))
    File.Delete(folderProvider.GetPath("worlds",
        world.Id + ".3mf"));

new Create3DWorldCommandHandler(new ThreeMfRepository(folderProvider)).Execute(
    new Create3DWorld(world.ToDto()));

File.Copy(folderProvider.GetPath("worlds",
    world.Id + ".3mf"), "c:\\Users\\Karim\\Coding\\WebProjects\\render-test\\template.3mf", true);