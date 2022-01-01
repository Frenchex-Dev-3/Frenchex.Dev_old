using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;
using Frenchex.Dev.Vos.Lib.Domain.Definitions;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.New;

public interface IDefineProvisioningNewCommandRequest : IRootCommandRequest
{
    string Name { get; }
    IDictionary<string, string> Env { get; }
    string[] Code { get; }
    OsTypeEnum OS { get; }
}