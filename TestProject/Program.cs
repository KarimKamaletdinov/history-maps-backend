using System.Drawing;
using HistoryMaps;


var rootFolder = new RootFolderProvider();

new SynchronizeWorldCommandHandler(new GetWorldCommandHandler(new WorldBmpRepository(rootFolder)),
    new Create3DWorldCommandHandler(new ThreeMfRepository(rootFolder)), rootFolder)
    .Execute(new SynchronizeWorld(Guid.Parse("96d68a4e-f97f-4823-89c9-b34f93020c5e")));