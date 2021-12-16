using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add
{
    public class DefineMachineTypeAddCommandRequest : IDefineMachineTypeAddCommandRequest
    {
        public IBaseRequest Base { get; }

        public MachineTypeDefinition Definition { get; }

        public DefineMachineTypeAddCommandRequest(
            IBaseRequest baseRequest,
            MachineTypeDefinition definition
        )
        {
            Base = baseRequest;
            Definition = definition;
        }
    }
}
