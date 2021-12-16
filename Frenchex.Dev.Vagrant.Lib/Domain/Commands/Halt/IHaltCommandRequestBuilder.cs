namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Halt
{
    public interface IHaltCommandRequestBuilder : Root.IRootCommandRequestBuilder
    {
        IHaltCommandRequest Build();
        IHaltCommandRequestBuilder UsingNamesOrIds(string[] namesOrIds);
        IHaltCommandRequestBuilder WithForce(bool with);
        IHaltCommandRequestBuilder UsingHaltTimeoutInMiliSeconds(int timeoutMs);
    }
}
