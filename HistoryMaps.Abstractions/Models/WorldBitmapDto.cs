using System.Drawing;

namespace HistoryMaps;

public record WorldBitmapDto(Bitmap Bitmap, IEnumerable<CountryColorDto> Countries);