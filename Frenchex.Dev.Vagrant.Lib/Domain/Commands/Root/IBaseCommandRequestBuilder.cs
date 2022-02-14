namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public interface IBaseCommandRequestBuilder
{
    IBaseCommandRequest Build();
    T Parent<T>() where T : IRootCommandRequestBuilder;
    IBaseCommandRequestBuilder WithColor(bool with);
    IBaseCommandRequestBuilder WithMachineReadable(bool with);
    IBaseCommandRequestBuilder WithVersion(bool with);
    IBaseCommandRequestBuilder WithDebug(bool with);
    IBaseCommandRequestBuilder WithTimestamp(bool with);
    IBaseCommandRequestBuilder WithTty(bool with);
    IBaseCommandRequestBuilder WithHelp(bool with);
    IBaseCommandRequestBuilder UsingWorkingDirectory(string? workingDirectory);
    IBaseCommandRequestBuilder UsingTimeoutMiliseconds(int timeoutMs);
}