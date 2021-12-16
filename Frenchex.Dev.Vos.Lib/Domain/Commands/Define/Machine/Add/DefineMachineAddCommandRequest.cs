using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add
{
    public class DefineMachineAddCommandRequest : RootRequest, IDefineMachineAddCommandRequest
    {
        public DefineMachineAddCommandRequest(
            MachineDefinition machineDefinition,
            IBaseRequest baseRequest
        ) : base(baseRequest)
        {
            Definition = machineDefinition;
        }
        public MachineDefinition Definition { get; }
    }
}
