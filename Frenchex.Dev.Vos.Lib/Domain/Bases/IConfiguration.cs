namespace Frenchex.Dev.Vos.Lib.Domain.Bases
{
    public interface IConfiguration
    {
        VagrantConfiguration Vagrant { get; }
        Dictionary<string, MachineTypeDefinition> MachineTypes { get; }
        Dictionary<string, MachineDefinition> Machines { get; }
    }
}