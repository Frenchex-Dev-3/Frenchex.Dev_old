namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public class SystemEnvironmentEntrypointInfo : IEntrypointInfo
{
    public string CommandLine => Environment.CommandLine;

    public string[] CommandLineArgs => Environment.GetCommandLineArgs();
}