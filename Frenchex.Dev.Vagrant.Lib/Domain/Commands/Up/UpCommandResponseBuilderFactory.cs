using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;

public class UpCommandResponseBuilderFactory : RootCommandResponseBuilderFactory, IUpCommandResponseBuilderFactory
{
    public IUpCommandResponseBuilder Build()
    {
        return new UpCommandResponseBuilder();
    }
}