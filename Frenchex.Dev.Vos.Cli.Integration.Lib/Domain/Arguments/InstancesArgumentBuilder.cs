using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Arguments;

public interface IInstancesArgumentBuilder
{
    Argument<int> Build();
}

public class InstancesArgumentBuilder : IInstancesArgumentBuilder
{
    public Argument<int> Build()
    {
        return new("instances", "# of instances");
    }
}