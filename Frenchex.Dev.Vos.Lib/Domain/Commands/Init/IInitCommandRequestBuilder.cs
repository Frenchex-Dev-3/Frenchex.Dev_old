using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Init;

public interface IInitCommandRequestBuilder : IRootCommandRequestBuilder
{
    IInitCommandRequest Build();
    IInitCommandRequestBuilder WithNamingPattern(string namingPattern);
    IInitCommandRequestBuilder WithGivenLeadingZeroes(int leadingZeroes);
}