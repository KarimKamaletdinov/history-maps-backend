namespace HistoryMaps;

public class Country
{
	private readonly List<Coordinate> _bound;

	public Country(Guid id, string name,
			IReadOnlyCollection<Coordinate> bound, Color color)
	{
		Id = id;
		Name = name;
		_bound = bound.ToList();
		Color = color;
	}

	public Guid Id { get; set; }
	public string Name { get; set; }
	public IReadOnlyCollection<Coordinate> Bound => _bound; 
	public Color Color { get; set; }

	public void AddBoundCoordinate(Coordinate coordinate)
	{
		_bound.Add(coordinate);
	}

	public void RemoveBoundCoordinate(Coordinate coordinate)
	{
		_bound.Remove(coordinate);
	}
}
