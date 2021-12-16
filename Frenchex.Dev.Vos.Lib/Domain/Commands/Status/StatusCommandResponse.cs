using Frenchex.Dev.Vagrant.Lib.Domain;
using System.Collections.Immutable;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status
{
    public class StatusCommandResponse : IStatusCommandResponse
    {
        public IImmutableDictionary<string, VagrantMachineStatusEnum> Statuses { get; }

        public StatusCommandResponse(
            IImmutableDictionary<string, VagrantMachineStatusEnum> statuses
        )
        {
            Statuses = statuses;
        }
    }
}
