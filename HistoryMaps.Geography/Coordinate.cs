namespace HistoryMaps;

/// <summary>
/// Географическая координата
/// </summary>
public class Coordinate
{

	private float _latitude;
	/// <summary>
	/// Широта
	/// </summary>
	public float Latitude
	{
		get => _latitude;
		set => _latitude = value >= 0 && value <= 90
			? value
			: throw new ValidationException(nameof(Latitude), value, "from 0 up to 90");
	}

	private float _longitude;

	/// <summary>
	/// Долгота
	/// </summary>
	public float Longitude
	{
		get => _longitude;
		set => _longitude = value >= 0 && value <= 90
			? value
			: throw new ValidationException(nameof(Latitude), value, "from 0 up to 180");
	}

	/// <summary>
	/// Географическая координата
	/// </summary>
	/// <param name="latitude">Долгота</param>
	/// <param name="longitude">Широта</param>
	public Coordinate(float latitude, float longitude)
	{
		Latitude = latitude;
		Longitude = longitude;
	}

	/// <summary>
	/// Географическая координата
	/// </summary>
	/// <param name="dto">Объект типа CoordinateDto c широтой и долготой</param>
	public Coordinate(CoordinateDto dto)
	{
		Latitude = dto.Latitude;
		Longitude = dto.Longitude;
	}

	/// <summary>
	/// Преобразует в объект типа CoordinateDto
	/// </summary>
	/// <returns>Объект типа CoordinateDto c широтой и долготой</returns>
	public CoordinateDto ToDto()
	{
		return new(Latitude, Longitude);
	}
}