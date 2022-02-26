using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IForceOptionBuilder
{
    Option<bool> Build();
}

public class ForceOptionBuilder : IForceOptionBuilder
{
    public Option<bool> Build()=> new(new[] { "--force", "-f" }, "Force");
}