using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public class DefineMachineTypeAddCommandRequest : IDefineMachineTypeAddCommandRequest
{
    public DefineMachineTypeAddCommandRequest(
        IBaseRequest baseRequest,
        MachineTypeDefinition definition
    )
    {
        Base = baseRequest;
        Definition = definition;
    }

    public IBaseRequest Base { get; }

    public MachineTypeDefinition Definition { get; }
}