namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommandResponseBuilderFactory : IDefineMachineAddCommandResponseBuilderFactory
{
    public IDefineMachineAddCommandResponseBuilder Factory()
    {
        return new DefineMachineAddCommandResponseBuilder();
    }
}