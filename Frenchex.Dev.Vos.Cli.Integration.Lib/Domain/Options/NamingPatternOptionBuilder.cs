using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface INamingPatternOptionBuilder
{
    Option<string> Build();
}

public class NamingPatternOptionBuilder : INamingPatternOptionBuilder
{
    public Option<string> Build()
    {
        return new Option<string>(
            new[] {"--naming-pattern", "-n"},
            () => "#vdi#-#name#-#instance#",
            "Naming pattern"
        );
    }
}