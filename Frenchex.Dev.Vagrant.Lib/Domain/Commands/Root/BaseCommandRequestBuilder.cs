﻿namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root
{
    public class BaseCommandRequestBuilder : IBaseCommandRequestBuilder
    {
        private readonly object _parent;

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

        public BaseCommandRequestBuilder(object parent)
        {
            _parent = parent;
        }

        public IBaseCommandRequest Build()
        {
            if (null == _workingDirectory)
            {
                throw new InvalidOperationException("_workingDirectory is null");
            }

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
                _timeoutMs
                );
        }

        public IBaseCommandRequestBuilder UsingTimeoutMiliseconds(int timeoutMs)
        {
            _timeoutMs = timeoutMs;
            return this;
        }

        public IBaseCommandRequestBuilder UsingWorkingDirectory(string workingDirectory)
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
        public IBaseCommandRequestBuilder WithDebugTimestamp(bool with)
        {
            _debugTimestamp = with;
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
            return (T)_parent;
        }
    }
}