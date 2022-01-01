using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public interface IStatusCommandRequestBuilder : IRootCommandRequestBuilder
{
    IStatusCommandRequest Build();
    IStatusCommandRequestBuilder WithNames(string[] name);
}