using System.Drawing;
using System.Runtime.CompilerServices;
using HistoryMaps;

//new ThreeMfRepository(new RootFolderProvider()).Insert(new Document
//{
//    Colors = new List<Color> {Color.Blue},
//    Metadata = "",
//    Triangles = new List<Triangle>{new (){Color = 0, V1 = 0, V2=1, V3=2}},
//    Vertices = new List<Vertex>{new(){X=-1, Y=-1, Z=0}, new(){X=1, Y= 0, Z=0}, new(){X=0, Y=0, Z= 0}}
//}, Guid.NewGuid());


var folderProvider = new RootFolderProvider();

var world = new WorldBmpRepository(folderProvider).Get(Guid.Parse("96d68a4e-f97f-4823-89c9-b34f93020c5e"),
    new Dictionary<string, Color> {{"water", Color.Black}});

var water = new AreaDto(world.Water.Points, Color.Blue);

if (File.Exists(folderProvider.GetPath("worlds",
        world.Id + ".3mf")))
    File.Delete(folderProvider.GetPath("worlds",
        world.Id + ".3mf"));

new Create3DWorldCommandHandler(new ThreeMfRepository(folderProvider)).Execute(
    new Create3DWorld(World: new WorldDto(world.Id, water, 
        Array.Empty<CountryDto>())));

File.Copy(folderProvider.GetPath("worlds",
    world.Id + ".3mf"), "c:\\Users\\Karim\\Coding\\WebProjects\\render-test\\template.3mf", true);