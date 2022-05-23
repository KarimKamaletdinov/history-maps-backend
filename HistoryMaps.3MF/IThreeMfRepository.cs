namespace HistoryMaps;

public interface IThreeMfRepository
{
    void Insert(Document document, Guid id);
}