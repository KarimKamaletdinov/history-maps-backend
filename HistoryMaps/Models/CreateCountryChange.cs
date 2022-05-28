namespace HistoryMaps;

public class CreateCountryChange : IChange
{
    public Country NewCountry { get; }

    public CreateCountryChange(Country newCountry)
    {
        NewCountry = newCountry;
    }

    public void Apply(World world)
    {
        world.Countries.Add(NewCountry);
    }

    public ChangeDto ToDto()
    {
        throw new NotImplementedException();
    }
}