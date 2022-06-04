namespace HistoryMaps;

/// <summary>
/// Обработчик команды
/// </summary>
/// <typeparam name="T">Тип обрабатываемой команды</typeparam>
public interface ICommandHandler<in T> where T : Command
{
    /// <summary>
    /// Выполнить команду
    /// </summary>
    /// <param name="command">Команда (с параметрами для выполнения)</param>
    void Execute(T command);
}