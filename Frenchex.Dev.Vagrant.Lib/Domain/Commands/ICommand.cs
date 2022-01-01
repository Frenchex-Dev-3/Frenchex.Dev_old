using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands;

public interface ICommand<U, R>
    where U : IRootCommandRequest
    where R : IRootCommandResponse
{
    R StartProcess(U request);
}