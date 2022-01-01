namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public class NameCommandResponseBuilderFactory : INameCommandResponseBuilderFactory
{
    public INameCommandResponseBuilder Factory()
    {
        return new NameCommandResponseBuilder();
    }
}