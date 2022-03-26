using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IVirtualRamMbOptionBuilder
{
    Option<int> Build();
}

internal class VirtualRamMbOptionBuilder : IVirtualRamMbOptionBuilder
{
    public Option<int> Build()
    {
        return new(new[] {"--vram-mb"}, () => 16, "VRAM in MB");
    }
}