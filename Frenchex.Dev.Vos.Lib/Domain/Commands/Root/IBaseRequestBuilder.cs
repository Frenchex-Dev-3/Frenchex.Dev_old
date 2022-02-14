namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

public interface IBaseRequestBuilder
{
    IBaseRequest Build();
    T Parent<T>();
    IBaseRequestBuilder SetParent(object parent);
    IBaseRequestBuilder WithColor(bool with);
    IBaseRequestBuilder WithMachineReadable(bool with);
    IBaseRequestBuilder WithVersion(bool with);
    IBaseRequestBuilder WithDebug(bool with);
    IBaseRequestBuilder WithTimestamp(bool with);
    IBaseRequestBuilder WithTty(bool with);
    IBaseRequestBuilder WithHelp(bool with);
    IBaseRequestBuilder UsingWorkingDirectory(string? workingDirectory);
    IBaseRequestBuilder UsingTimeoutMiliseconds(int timeoutMs);
}