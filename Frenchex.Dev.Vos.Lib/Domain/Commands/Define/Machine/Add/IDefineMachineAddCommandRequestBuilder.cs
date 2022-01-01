using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Configuration;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add;

public interface IDefineMachineAddCommandRequestBuilder : IRootCommandRequestBuilder
{
    IDefineMachineAddCommandRequestBuilder UsingDefinition(MachineDefinitionDeclaration definitionDeclaration);
    IDefineMachineAddCommandRequest Build();
}