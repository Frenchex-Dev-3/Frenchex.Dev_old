using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init
{
    public interface IInitCommandRequestBuilder : IRootCommandRequestBuilder
    {
        IInitCommandRequest Build();
        IInitCommandRequestBuilder WithInstanceNumber(int instanceNumber);
        IInitCommandRequestBuilder WithNamingPattern(string namingPattern);
    }
}
