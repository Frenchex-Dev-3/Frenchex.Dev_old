using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status;

public class StatusCommandResponseBuilderFactory : RootCommandResponseBuilderFactory,
    IStatusCommandResponseBuilderFactory
{
    public IStatusCommandResponseBuilder Build()
    {
        return new StatusCommandResponseBuilder();
    }
}