namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public class DefineMachineTypeAddCommandResponseBuilder : IDefineMachineTypeAddCommandResponseBuilder
{
    public IDefineMachineTypeAddCommandResponse Build()
    {
        return new DefineMachineTypeAddCommandResponse();
    }
}