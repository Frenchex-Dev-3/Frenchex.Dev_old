namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public interface IVagrantPluginsConfiguration
    {
        string? Version { get; }
        bool? Enabled { get; }
        Dictionary<string, object>? Configuration { get; }
    }
}
