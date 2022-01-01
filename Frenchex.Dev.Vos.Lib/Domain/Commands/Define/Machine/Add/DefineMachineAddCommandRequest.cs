using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Configuration;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;

public class DefineMachineAddCommandRequest : RootRequest, IDefineMachineAddCommandRequest
{
    public DefineMachineAddCommandRequest(
        MachineDefinitionDeclaration machineDefinitionDeclaration,
        IBaseRequest baseRequest
    ) : base(baseRequest)
    {
        DefinitionDeclaration = machineDefinitionDeclaration;
    }

    public MachineDefinitionDeclaration DefinitionDeclaration { get; }
}