namespace HistoryMaps;

public interface IQueryHandler<in T, TResult> where T : Query<TResult>
{
    Task<TResult> Execute(T query);
}