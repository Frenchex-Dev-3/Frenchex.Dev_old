using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;

public class UpCommandRequestBuilderFactory : IUpCommandRequestBuilderFactory
{
    private readonly IBaseCommandRequestBuilderFactory _baseFactory;

    public UpCommandRequestBuilderFactory(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        _baseFactory = baseRequestBuilderFactory;
    }

    public IUpCommandRequestBuilder Factory()
    {
        return new UpCommandRequestBuilder(_baseFactory);
    }
}