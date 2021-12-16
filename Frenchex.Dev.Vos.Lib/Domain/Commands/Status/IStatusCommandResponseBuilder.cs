using Frenchex.Dev.Vagrant.Lib.Domain;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using System.Collections.Immutable;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Status
{
    public interface IStatusCommandResponseBuilder : IRootResponseBuilder
    {
        IStatusCommandResponse Build();
        IStatusCommandResponseBuilder WithStatuses(IImmutableDictionary<string, VagrantMachineStatusEnum> statuses);
    }
}
