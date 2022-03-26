using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IVagrantBinPathOptionBuilder
{
    Option<string> Build();
}

public class VagrantBinPathOptionBuilder : IVagrantBinPathOptionBuilder
{
    public Option<string> Build()
    {
        return new(new[] {"--vagrant-bin-path"}, "Vagrant Bin Path");
    }
}