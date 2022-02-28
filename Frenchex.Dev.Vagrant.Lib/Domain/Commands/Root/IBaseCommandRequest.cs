namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public interface IBaseCommandRequest
{
    bool Color { get; }
    bool MachineReadable { get; }
    bool Version { get; }
    bool Debug { get; }
    bool Timestamp { get; }
    bool DebugTimestamp { get; }
    bool Tty { get; }
    bool Help { get; }
    string? WorkingDirectory { get; }
    int TimeoutInMiliSeconds { get; }
    string VagrantBinPath { get; }
}