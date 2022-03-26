namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

public class BaseCommandRequestBuilder : IBaseCommandRequestBuilder
{
    private readonly object _parent;
    private bool? _color;
    private bool? _debug;
    private bool? _debugTimestamp;
    private bool? _help;
    private bool? _machineReadable;

    private int? _timeoutMs;
    private bool? _timestamp;
    private bool? _tty;
    private string? _vagrantBinpath;
    private bool? _version;
    private string? _workingDirectory;

    public BaseCommandRequestBuilder(object parent)
    {
        _parent = parent;
    }

    public IBaseCommandRequest Build()
    {
        if (null == _workingDirectory)
            throw new InvalidOperationException("_workingDirectory is null");

        return new BaseCommandRequest(
            _workingDirectory,
            _color,
            _machineReadable,
            _version,
            _debug,
            _timestamp,
            _debugTimestamp,
            _tty,
            _help,
            _timeoutMs,
            _vagrantBinpath
        );
    }

    public IBaseCommandRequestBuilder UsingTimeoutMiliseconds(int timeoutMs)
    {
        _timeoutMs = timeoutMs;
        return this;
    }

    public IBaseCommandRequestBuilder UsingWorkingDirectory(string? workingDirectory)
    {
        _workingDirectory = workingDirectory;
        return this;
    }

    public IBaseCommandRequestBuilder WithColor(bool with)
    {
        _color = with;
        return this;
    }

    public IBaseCommandRequestBuilder WithDebug(bool with)
    {
        _debug = with;
        return this;
    }

    public IBaseCommandRequestBuilder WithHelp(bool with)
    {
        _help = with;
        return this;
    }

    public IBaseCommandRequestBuilder WithMachineReadable(bool with)
    {
        _machineReadable = with;
        return this;
    }

    public IBaseCommandRequestBuilder WithTimestamp(bool with)
    {
        _timestamp = with;
        return this;
    }

    public IBaseCommandRequestBuilder WithTty(bool with)
    {
        _tty = with;
        return this;
    }

    public IBaseCommandRequestBuilder WithVersion(bool with)
    {
        _version = with;
        return this;
    }

    public T Parent<T>() where T : IRootCommandRequestBuilder
    {
        return (T) _parent;
    }

    public IBaseCommandRequestBuilder WithDebugTimestamp(bool with)
    {
        _debugTimestamp = with;
        return this;
    }

    public IBaseCommandRequestBuilder WithVagrantBinPath(string vagrantBinPath)
    {
        _vagrantBinpath = vagrantBinPath;
        return this;
    }
}