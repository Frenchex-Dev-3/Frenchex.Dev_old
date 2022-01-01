using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public interface ISshCommandRequestBuilder : IRootCommandRequestBuilder
{
    ISshCommandRequest Build();
    ISshCommandRequestBuilder UsingName(string nameOrId);
    ISshCommandRequestBuilder UsingCommand(string command);
    ISshCommandRequestBuilder WithPlain(bool with);
    ISshCommandRequestBuilder UsingExtraSshArgs(string extraSshArg);
}