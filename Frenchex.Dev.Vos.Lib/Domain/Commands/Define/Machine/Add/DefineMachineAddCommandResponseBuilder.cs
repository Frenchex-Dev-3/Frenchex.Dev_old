namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommandResponseBuilder : IDefineMachineAddCommandResponseBuilder
{
    public IDefineMachineAddCommandResponse Build()
    {
        return new DefineMachineAddCommandResponse();
    }
}