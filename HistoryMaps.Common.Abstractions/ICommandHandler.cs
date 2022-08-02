namespace HistoryMaps;

public interface ICommandHandler<in T> where T : Command
{
    Task Execute(T command);
}