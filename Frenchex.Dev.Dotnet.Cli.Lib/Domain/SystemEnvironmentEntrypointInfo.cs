namespace Frenchex.Dev.Dotnet.Cli.Lib.Domain
{
    /// <summary>Implements <see cref="IEntrypointInfo"/> by exposing data provided by <see cref="Environment"/>.</summary>
    public class SystemEnvironmentEntrypointInfo : IEntrypointInfo
    {
        public string CommandLine => Environment.CommandLine;

        public string[] CommandLineArgs => Environment.GetCommandLineArgs();
    }
}
