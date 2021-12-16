using Frenchex.Dev.Vagrant.Lib.Domain;
using System.Collections.Immutable;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status
{
    public class StatusCommandResponseBuilder : IStatusCommandResponseBuilder
    {
        public IStatusCommandResponse Build()
        {
            return new StatusCommandResponse(
                _statuses ?? new Dictionary<string, VagrantMachineStatusEnum>().ToImmutableDictionary()
            );
        }

        private IImmutableDictionary<string, VagrantMachineStatusEnum>? _statuses;
        public IStatusCommandResponseBuilder WithStatuses(IImmutableDictionary<string, VagrantMachineStatusEnum> statuses)
        {
            _statuses = statuses;
            return this;
        }
    }
}
