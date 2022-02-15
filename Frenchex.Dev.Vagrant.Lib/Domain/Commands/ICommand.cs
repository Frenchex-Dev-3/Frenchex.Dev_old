using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands;

public interface ICommand<in TU, out TR>
    where TU : IRootCommandRequest
    where TR : IRootCommandResponse
{
    TR StartProcess(TU request);
}