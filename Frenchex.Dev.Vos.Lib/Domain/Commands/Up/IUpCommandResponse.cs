using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Up;

public interface IUpCommandResponse : IRootCommandResponse
{
    Vagrant.Lib.Domain.Commands.Up.IUpCommandResponse Response { get; }
}