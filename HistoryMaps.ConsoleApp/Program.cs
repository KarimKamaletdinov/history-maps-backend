using CommandLine;
using HistoryMaps;

Console.WriteLine(
    Parser.Default.ParseArguments<SyncWorld>(args).Value.WorldId);
while (true)
{
    Parser.Default.ParseArguments<SyncWorld, int>(Console.ReadLine().Split(' '))
        .WithParsed(ParseArg);
}

void ParseArg(object arg)
{
    switch (arg)
    {
        case SyncWorld s:
            Console.WriteLine(s.WorldId);
            break;
        case int i:
            Console.WriteLine(i);
            break;
    }
}