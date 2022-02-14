namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Commands.Define.MachineType.Add
{
    public class DefineMachineTypeAddCommandIntegrationPayload
    {
        public string? Name { get; set; }
        public string? BoxName { get; set; }
        public string? OsType { get; set; }
        public string? OsVersion { get; set; }
        public int VCpus { get; set; }
        public int RamInMb { get; set; }
        public bool Enabled { get; set; }
        public bool Enable3D { get; set; }
        public int TimeOutMs { get; set; }
        public string? WorkingDirectory { get; set; }
    }
}
