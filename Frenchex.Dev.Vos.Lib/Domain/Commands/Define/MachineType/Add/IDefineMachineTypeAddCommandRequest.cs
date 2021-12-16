using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add
{
    public interface IDefineMachineTypeAddCommandRequest : IRootCommandRequest
    {
        MachineTypeDefinition Definition { get; }
    }
}
