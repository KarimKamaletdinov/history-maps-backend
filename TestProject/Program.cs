using HistoryMaps;

File.WriteAllBytes("test.stl", StlSerializer.StlToBytes(new StlDocument(new[]
{
    new Triangle(
        new (1,1,1),
        new(2,3,1),
        new(3,1,1),
        new Color(255, 0,0)
        )
})));