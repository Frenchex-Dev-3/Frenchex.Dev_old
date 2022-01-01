using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public class UpCommandResponseBuilderFactory : RootResponseBuilderFactory, IUpCommandResponseBuilderFactory
{
    public IUpCommandResponseBuilder Factory()
    {
        return new UpCommandResponseBuilder();
    }
}