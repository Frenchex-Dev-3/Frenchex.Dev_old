namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up
{
    public interface IUpCommandRequestBuilder : Root.IRootCommandRequestBuilder
    {
        IUpCommandRequest Build();
        IUpCommandRequestBuilder UsingNamesOrIds(string[] namesOrIds);
        IUpCommandRequestBuilder WithProvision(bool with);
        IUpCommandRequestBuilder UsingProvisionWith(string[] provisionWith);
        IUpCommandRequestBuilder WithDestroyOnError(bool with);
        IUpCommandRequestBuilder WithParallel(bool with);
        IUpCommandRequestBuilder UsingProvider(string provider);
        IUpCommandRequestBuilder WithInstallProvider(bool with);
    }

}
