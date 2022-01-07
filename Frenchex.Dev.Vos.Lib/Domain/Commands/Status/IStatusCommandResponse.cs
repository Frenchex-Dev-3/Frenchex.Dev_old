using System.Collections.Immutable;
using Frenchex.Dev.Vagrant.Lib.Domain;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public interface IStatusCommandResponse : IRootCommandResponse
{
    IImmutableDictionary<string, (string, VagrantMachineStatusEnum)> Statuses { get; }
}