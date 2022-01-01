using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.MachineType.Add;

public interface IDefineMachineTypeAddCommandRequestBuilder : IRootCommandRequestBuilder
{
    IDefineMachineTypeAddCommandRequestBuilder UsingDefinition(MachineTypeDefinition definition);
    IDefineMachineTypeAddCommandRequest Build();
}