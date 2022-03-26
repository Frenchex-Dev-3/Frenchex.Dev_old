using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IZeroesOptionBuilder
{
    Option<int> Build();
}

public class ZeroesOptionBuilder : IZeroesOptionBuilder
{
    public Option<int> Build()
    {
        return new(
            new[] {"--zeroes", "-z"},
            () => 2,
            "Numbering leading zeroes"
        );
    }
}