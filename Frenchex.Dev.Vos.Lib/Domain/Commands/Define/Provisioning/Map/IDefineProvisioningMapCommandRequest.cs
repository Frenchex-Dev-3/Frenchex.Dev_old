using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map;

public interface IDefineProvisioningMapCommandRequest : IRootCommandRequest
{
    string Provisioning { get; }
    IDictionary<string, string> Env { get; }
}