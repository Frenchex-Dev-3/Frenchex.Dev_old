using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public interface IDefineMachineTypeAddCommandRequest : IRootCommandRequest
{
    MachineTypeDefinition Definition { get; }
}