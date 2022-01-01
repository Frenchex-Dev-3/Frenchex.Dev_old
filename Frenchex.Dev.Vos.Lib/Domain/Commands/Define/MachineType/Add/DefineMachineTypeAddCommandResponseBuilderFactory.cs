namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public class DefineMachineTypeAddCommandResponseBuilderFactory : IDefineMachineTypeAddCommandResponseBuilderFactory
{
    public IDefineMachineTypeAddCommandResponseBuilder Factory()
    {
        return new DefineMachineTypeAddCommandResponseBuilder();
    }
}