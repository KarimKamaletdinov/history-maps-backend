using System.Drawing;

namespace HistoryMaps;

public class World
{
    private readonly List<Country> _countries;


    public Guid Id { get; set; }

    public Area Water { get; }

    public IReadOnlyCollection<Country> Countries => _countries;

    public World(Guid id, Area water, IEnumerable<Country> countries)
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
    public void AddPixel(int latitude, int longitude, Country country)
    {
        if (!Countries.Contains(country))
            throw new DomainException("Нет такой страны");
        if (Water.GetPixel(latitude, longitude))
            throw new DomainException("Нельзя задать стране пиксель, явлиющийся водой");
        if(country.GetPixel(latitude, longitude))
            throw new DomainException("Пиксель уже относится к этой стране");
        foreach (var c in Countries)
            c.SetPixel(latitude, longitude, false);
        country.SetPixel(latitude, longitude, true);
    }

    

    /// <summary>
    /// Очистить пиксель
    /// </summary>
    /// <param name="latitude">Широта</param>
    /// <param name="longitude">Долгота</param>
    public void ClearPixel(int latitude, int longitude)
    {
        if (Water.GetPixel(latitude, longitude))
            throw new DomainException("Нельзя задать стране пиксель, явлиющийся водой");
        foreach (var c in Countries)
            c.SetPixel(latitude, longitude, false);
    }
}