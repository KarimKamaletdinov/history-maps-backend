namespace HistoryMaps;

public class ConquestChange : IChange
{
    private readonly Area? _conqueredArea;
    public Country Conqueror { get; set; }
    public Country? Conquered { get; }

    public Area? ConqueredArea
    {
        get => _conqueredArea;
        private init
        {
            if (value == null)
            {
                _conqueredArea = value;
                return;
            }
            for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    if (value.Points[x, y])
                    {
                        if (Conquered != null && !Conquered.Points[x, y])
                            throw new DomainException("Conquered country doesn't contain" +
                                                      $" the point ({x}, {y})");
                        if (Conqueror.Points[x, y])
                            throw new DomainException("Conqueror country already contains" +
                                                      $" the point ({x}, {y})");
                    }

            _conqueredArea = value;
        }
    }

    public ConquestChange(Country conqueror, Country? conquered, Area? conqueredArea)
    {
        if (conquered == null && conqueredArea == null)
            throw new DomainException("You must specify at least one of these:" +
                                      $"{nameof(conquered)}, {nameof(conqueredArea)}");
        Conqueror = conqueror;
        Conquered = conquered;
        ConqueredArea = conqueredArea;
    }

    public void Apply(World world)
    {
        if (ConqueredArea == null)
        {
            for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    if (Conquered != null && Conquered.Points[x, y])
                        world.SetPoint(x, y, Conqueror);
        }
        else
        {
            for (var x = 0; x < Map.Width; x++)
                for (var y = 0; y < Map.Height; y++)
                    if (ConqueredArea != null && ConqueredArea.Points[x, y])
                        world.SetPoint(x, y, Conqueror);
        }
    }

    public ChangeDto ToDto()
    {
        throw new NotImplementedException();
    }
}