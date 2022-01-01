namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

public interface IBaseRequest
{
    bool Color { get; }
    bool MachineReadable { get; }
    bool Version { get; }
    bool Debug { get; }
    bool Timestamp { get; }
    bool DebugTimestamp { get; }
    bool Tty { get; }
    bool Help { get; }
    string WorkingDirectory { get; }
    int TimeoutInMiliSeconds { get; }
    T Parent<T>(T parent);
}