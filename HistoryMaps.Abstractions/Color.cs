namespace HistoryMaps;

/// <summary>
/// Цвет (RGB)
/// </summary>
public struct Color
{
	/// <summary>
	/// Красный
	/// </summary>
	public byte R { get; set; }
	/// <summary>
	/// Зелёный
	/// </summary>
	public byte G { get; set; }
	/// <summary>
	/// Синий
	/// </summary>
	public byte B { get; set; }

	/// <summary>
	/// Приводит к строке rgb-цвета для css
	/// </summary>
	/// <returns>Строка rgb-цвета для css (например "rgb(255, 255, 0)")</returns>
	public string ToRgb()
	{
		return $"rgb({R},{G},{B})";
	}

	/// <summary>
	/// Приводит к строке hex-цвета для css
	/// </summary>
	/// <returns>Строка hex-цвета для css (например "#ffff00")</returns>
	public string ToHex()
	{
		return $"#{R:X2}{G:X2}{B:X2}";
	}

	/// <summary>
	/// Приводит к строке
	/// </summary>
	/// <returns>Строка вида "{R, G, B}" (например "{255, 255, 0}")</returns>
	public override string ToString()
	{
		return $"{{{R}, {G}, {B}}}";
	}
}
