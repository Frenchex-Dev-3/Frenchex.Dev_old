using System.CommandLine;

namespace Frenchex.Dev.Dotnet.Cli.Integration.Lib.Domain
{
    public interface IIntegration
    {
        void Integrate(RootCommand rootCommand);
    }
}
