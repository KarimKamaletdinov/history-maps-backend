namespace HistoryMaps;

/// <summary>
/// Область на карте
/// </summary>
public class Area
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// Название области
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Точки [широта, долгота].
    /// Широт 181 = 90 северных + 1 экватор + 90 южных.
    /// Долгот 361: 180 западных + 1 экватор + 180 восточных.
    /// </summary>
    public bool[,] Points = new bool[181, 361];

    public Area(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}