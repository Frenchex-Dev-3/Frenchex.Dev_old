namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

public abstract class RootCommandRequestBuilderFactory : IRootCommandRequestBuilderFactory
{
    protected readonly IBaseRequestBuilderFactory _baseRequestBuilderFactory;

    public RootCommandRequestBuilderFactory(
        IBaseRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        _baseRequestBuilderFactory = baseRequestBuilderFactory;
    }
}