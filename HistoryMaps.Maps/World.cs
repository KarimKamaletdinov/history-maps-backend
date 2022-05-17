namespace HistoryMaps;

public class World
{
    private readonly List<Area> _countries;


    public Guid Id { get; set; }

    public Area Water { get; }

    public IReadOnlyCollection<Area> Countries => _countries;

    public World(Guid id,Area water, IReadOnlyCollection<Area> countries)
    {
        Id = id;
        Water = water;
        _countries = countries.ToList();
    }

    /// <summary>
    /// Задать пиксель стране
    /// </summary>
    /// <param name="latitude">Широта</param>
    /// <param name="longitude">Долгота</param>
    /// <param name="country">Страна</param>
    public void SetPixel(int latitude, int longitude, Area country)
    {
        if (!Countries.Contains(country))
            throw new DomainException("Нет такой страны");
        if (Water.Points[latitude, longitude])
            throw new DomainException("Нельзя задать стране пиксель, явлиющийся водой");
        if(country.Points[latitude, longitude])
            throw new DomainException("Пиксель уже относится к этой стране");
        foreach (var c in Countries)
            c.Points[latitude, longitude] = false;
        country.Points[latitude, longitude] = true;
    }
}