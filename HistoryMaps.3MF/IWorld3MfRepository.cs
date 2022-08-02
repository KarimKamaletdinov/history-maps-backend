namespace HistoryMaps;

public interface IWorld3MfRepository
{
    Task InsertSeparately(WorldDto world);
    Task ClearAll();
    Task<IEnumerable<Guid>> GetAllIds();
}