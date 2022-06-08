namespace HistoryMaps;

public class DropCountryChange : IChange
{
    public string DroppedCountryName { get; }

    public DropCountryChange(string droppedCountryName)
    {
        DroppedCountryName = droppedCountryName;
    }

    public void Apply(World world)
    {
        world.Countries.Remove(world.Countries.First(x => x.Name == DroppedCountryName));
    }

    public ChangeDto ToDto()
    {
        return new(ChangeType.DropCountry, DroppedCountryName, null, Guid.Empty);
    }
}