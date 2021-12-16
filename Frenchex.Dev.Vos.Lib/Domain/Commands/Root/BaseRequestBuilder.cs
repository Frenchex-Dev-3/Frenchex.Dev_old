namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root
{
    public class BaseRequestBuilder : IBaseRequestBuilder
    {
        private object? _parent;

        private int? _timeoutMs;
        private string? _workingDirectory;
        private bool? _color;
        private bool? _debug;
        private bool? _debugTimestamp;
        private bool? _tty;
        private bool? _help;
        private bool? _machineReadable;
        private bool? _version;
        private bool? _timestamp;
        public BaseRequestBuilder()
        {

        }

        public IBaseRequest Build()
        {
            return new BaseRequest(
                _color ?? false,
                _machineReadable ?? false,
                _version ?? false,
                _debug ?? false,
                _timestamp ?? false,
                _debugTimestamp ?? false,
                _tty ?? false,
                _help ?? false,
                _workingDirectory ?? "",
                _timeoutMs ?? (int)TimeSpan.FromMinutes(10).TotalMilliseconds
            );
        }

        public IBaseRequestBuilder UsingTimeoutMiliseconds(int timeoutMs)
        {
            _timeoutMs = timeoutMs;
            return this;
        }

        public IBaseRequestBuilder UsingWorkingDirectory(string workingDirectory)
        {
            _workingDirectory = workingDirectory;
            return this;
        }

        public IBaseRequestBuilder WithColor(bool with)
        {
            _color = with;
            return this;
        }

        public IBaseRequestBuilder WithDebug(bool with)
        {
            _debug = with;
            return this;
        }
        public IBaseRequestBuilder WithDebugTimestamp(bool with)
        {
            _debugTimestamp = with;
            return this;
        }

        public IBaseRequestBuilder WithHelp(bool with)
        {
            _help = with;
            return this;
        }

        public IBaseRequestBuilder WithMachineReadable(bool with)
        {
            _machineReadable = with;
            return this;
        }

        public IBaseRequestBuilder WithTimestamp(bool with)
        {
            _timestamp = with;
            return this;
        }

        public IBaseRequestBuilder WithTty(bool with)
        {
            _tty = with;
            return this;
        }

        public IBaseRequestBuilder WithVersion(bool with)
        {
            _version = with;
            return this;
        }

        public T Parent<T>()
        {
            if (null == _parent)
            {
                throw new InvalidOperationException("parent is null");
            }

            return (T)_parent;
        }

        public IBaseRequestBuilder SetParent(object parent)
        {
            _parent = parent;
            return this;
        }
    }
}
