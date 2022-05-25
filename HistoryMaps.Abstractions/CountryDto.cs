using System.Drawing;

namespace HistoryMaps;

public record CountryDto(string Name, Color Color):AreaDto(Color);