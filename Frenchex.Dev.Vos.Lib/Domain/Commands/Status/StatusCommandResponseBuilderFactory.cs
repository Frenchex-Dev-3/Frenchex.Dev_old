namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public class StatusCommandResponseBuilderFactory : IStatusCommandResponseBuilderFactory
{
    public IStatusCommandResponseBuilder Factory()
    {
        return new StatusCommandResponseBuilder();
    }
}