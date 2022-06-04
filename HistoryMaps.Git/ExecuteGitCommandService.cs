using System.Diagnostics;

namespace HistoryMaps;

public class ExecuteGitCommandService
{
    public void ExecuteGitCommand(string gitCommand, string? workingDirectory = null)
    {
        using var process = new Process();
        process.StartInfo.FileName = "git";
        process.StartInfo.Arguments = gitCommand;
        if(workingDirectory != null)
            process.StartInfo.WorkingDirectory = workingDirectory;
        process.Start();
        process.WaitForExit();
    }
}