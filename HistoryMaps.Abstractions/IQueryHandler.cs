namespace HistoryMaps;

public interface IQueryHandler<in T, out TResult> where T : Query<TResult>
{
    TResult Execute(T command);
}