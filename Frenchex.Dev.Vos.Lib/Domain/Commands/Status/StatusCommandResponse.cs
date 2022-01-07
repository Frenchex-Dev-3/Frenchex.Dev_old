using System.Collections.Immutable;
using Frenchex.Dev.Vagrant.Lib.Domain;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status;

public class StatusCommandResponse : IStatusCommandResponse
{
    public StatusCommandResponse(
        IImmutableDictionary<string, (string, VagrantMachineStatusEnum)> statuses
    )
    {
        Statuses = statuses;
    }

    public IImmutableDictionary<string, (string, VagrantMachineStatusEnum)> Statuses { get; }
}