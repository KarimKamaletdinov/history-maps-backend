namespace HistoryMaps;

/// <summary>
/// Обработчик запроса
/// </summary>
/// <typeparam name="T">Тип запроса</typeparam>
/// <typeparam name="TResult">Тип возвращаемого значения</typeparam>
public interface IQueryHandler<in T, out TResult> where T : Query<TResult>
{
    /// <summary>
    /// Выполнить запрос
    /// </summary>
    /// <param name="query">Запрос</param>
    /// <returns>Вычисленное или полученное значение по запросу</returns>
    TResult Execute(T query);
}