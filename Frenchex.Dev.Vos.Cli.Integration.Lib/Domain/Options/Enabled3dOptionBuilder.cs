using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IEnabled3dOptionBuilder
{
    Option<bool> Build();
}

public class Enabled3dOptionBuilder : IEnabled3dOptionBuilder
{
    public Option<bool> Build() => new(new[] {"--enabled-3d", "-3"}, "Enable Machine Type");
}