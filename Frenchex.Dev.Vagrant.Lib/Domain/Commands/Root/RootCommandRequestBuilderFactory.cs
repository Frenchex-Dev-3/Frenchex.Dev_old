namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public abstract class RootCommandRequestBuilderFactory : IRootCommandRequestBuilderFactory
{
    protected readonly IBaseCommandRequestBuilderFactory _baseRequestBuilderFactory;

    public RootCommandRequestBuilderFactory(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        _baseRequestBuilderFactory = baseRequestBuilderFactory;
    }
}