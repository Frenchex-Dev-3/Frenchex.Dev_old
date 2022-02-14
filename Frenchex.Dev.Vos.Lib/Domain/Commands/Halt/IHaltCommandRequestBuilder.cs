using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt;

public interface IHaltCommandRequestBuilder : IRootCommandRequestBuilder
{
    IHaltCommandRequest Build();
    IHaltCommandRequestBuilder UsingNames(string[]? names);
    IHaltCommandRequestBuilder WithForce(bool with);
    IHaltCommandRequestBuilder UsingHaltTimeoutInMiliSeconds(int timeoutMs);
}