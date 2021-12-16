namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    public interface IEntrypointInfo
    {
        string CommandLine { get; }

        string[] CommandLineArgs { get; }

        // Default interface implementation, requires C# 8.0 or later:
        bool HasFlag(string flagName)
        {
            return CommandLineArgs.Any(a => "-" + a == flagName || "/" + a == flagName);
        }
    }
}
