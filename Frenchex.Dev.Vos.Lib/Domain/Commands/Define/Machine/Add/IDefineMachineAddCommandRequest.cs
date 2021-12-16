using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add
{
    public interface IDefineMachineAddCommandRequest : IRootCommandRequest
    {
        MachineDefinition Definition { get; }
    }
}
