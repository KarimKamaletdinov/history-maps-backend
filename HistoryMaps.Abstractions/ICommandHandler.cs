namespace HistoryMaps;

public interface ICommandHandler<in T> where T : Command
{
    void Execute(T command);
}