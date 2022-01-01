using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh;

public interface ISshCommandRequestBuilderFactory : IRootCommandRequestBuilderFactory
{
    ISshCommandRequestBuilder Factory();
}