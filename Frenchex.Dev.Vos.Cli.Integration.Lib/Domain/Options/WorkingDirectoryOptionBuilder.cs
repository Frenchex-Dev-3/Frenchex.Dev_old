using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IWorkingDirectoryOptionBuilder
{
    Option<string> Build();
}

public class WorkingDirectoryOptionBuilder : IWorkingDirectoryOptionBuilder
{
    public Option<string> Build()=> new(
        new[] { "--working-directory", "-w" },
        Directory.GetCurrentDirectory,
        "Working Directory"
    );
}