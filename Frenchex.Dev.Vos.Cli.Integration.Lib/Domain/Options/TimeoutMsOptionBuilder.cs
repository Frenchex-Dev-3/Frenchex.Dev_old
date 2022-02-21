using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface ITimeoutMsOptionBuilder
{
    Option<int> Build();
    Option<int> Build(string[] aliases, Func<int> getDefaultFunc, string description);
}

public class TimeoutMsOptionBuilder : ITimeoutMsOptionBuilder
{
    public Option<int> Build()
    {
        return Build(
            new[] {"--timeout-ms", "-t"},
            () => (int) TimeSpan.FromMinutes(10).TotalMilliseconds,
            "TimeOut in ms"
        );
    }

    public Option<int> Build(string[] aliases, Func<int> getDefaultFunc, string description)
    {
        return new(
            aliases,
            getDefaultFunc,
            description
        );
    }
}