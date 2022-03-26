namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public class BaseCommandRequest : IBaseCommandRequest
{
    public BaseCommandRequest(
        string? workingDirectory,
        bool? color,
        bool? machineReadable,
        bool? version,
        bool? debug,
        bool? timestamp,
        bool? debugTimestamp,
        bool? tty,
        bool? help,
        int? timeoutMs,
        string? vagrantBinPath
    )
    {
        WorkingDirectory = workingDirectory;
        Color = color ?? false;
        MachineReadable = machineReadable ?? false;
        Version = version ?? false;
        Debug = debug ?? false;
        Timestamp = timestamp ?? false;
        DebugTimestamp = debugTimestamp ?? false;
        Tty = tty ?? false;
        Help = help ?? false;
        TimeoutInMiliSeconds = timeoutMs ?? (int) TimeSpan.FromMinutes(1).TotalMilliseconds;
        VagrantBinPath = vagrantBinPath ?? "vagrant";
    }

    public bool Color { get; }
    public bool MachineReadable { get; }
    public bool Version { get; }
    public bool Debug { get; }
    public bool Timestamp { get; }
    public bool DebugTimestamp { get; }
    public bool Tty { get; }
    public bool Help { get; }
    public string? WorkingDirectory { get; }
    public int TimeoutInMiliSeconds { get; }
    public string VagrantBinPath { get; }
}