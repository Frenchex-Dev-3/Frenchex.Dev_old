namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain;

public interface IEntrypointInfo
{
    string CommandLine { get; }

    string[] CommandLineArgs { get; }
}