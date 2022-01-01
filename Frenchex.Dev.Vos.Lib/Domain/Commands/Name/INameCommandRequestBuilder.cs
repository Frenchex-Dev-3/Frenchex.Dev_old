using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public interface INameCommandRequestBuilder : IRootCommandRequestBuilder
{
    INameCommandRequest Build();
    INameCommandRequestBuilder WithNames(string[] names);
}