using System.Net;
using System.Net.NetworkInformation;

namespace Frenchex.Dev.Vos.Lib.Domain.Actions.Networking;

public interface IDefaultGatewayGetterAction
{
    List<(NetworkInterface n, List<IPAddress?>?)> GetDefaultGateway();
}

public class DefaultGatewayGetterAction : IDefaultGatewayGetterAction
{
    public List<(NetworkInterface n, List<IPAddress?>?)> GetDefaultGateway()
    {
        return NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(n => n.OperationalStatus == OperationalStatus.Up)
            .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(n => (n, n.GetIPProperties()?.GatewayAddresses.Select(x => x?.Address).ToList()))
            .Where(x => x.Item2.Any())
            .ToList();
    }
}