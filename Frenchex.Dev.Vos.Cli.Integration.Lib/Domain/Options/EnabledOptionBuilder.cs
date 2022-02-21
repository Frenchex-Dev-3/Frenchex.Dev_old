using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IEnabledOptionBuilder
{
    Option<bool> Build();
}

public class EnabledOptionBuilder : IEnabledOptionBuilder
{
    public Option<bool> Build()
    {
        return new(new[] {"--enabled", "-e"}, "Enabled");
    }
}