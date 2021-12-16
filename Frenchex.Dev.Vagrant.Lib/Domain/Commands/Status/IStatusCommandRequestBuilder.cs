namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Status
{

    public interface IStatusCommandRequestBuilder : Root.IRootCommandRequestBuilder
    {
        IStatusCommandRequest Build();
        IStatusCommandRequestBuilder WithNamesOrIds(string[] namesOrIds);
    }

}
