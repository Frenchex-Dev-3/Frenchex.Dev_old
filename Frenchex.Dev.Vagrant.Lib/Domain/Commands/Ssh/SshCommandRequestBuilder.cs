using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh;

public class SshCommandRequestBuilder : RootCommandRequestBuilder, ISshCommandRequestBuilder
{
    private string? _command;
    private string? _extraSshArgs;
    private string? _nameOrId;
    private bool? _plain;

    public SshCommandRequestBuilder(
        IBaseCommandRequestBuilderFactory? baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public ISshCommandRequest Build()
    {
        return new SshCommandRequest(
            _nameOrId ?? "",
            _command ?? "",
            _plain ?? false,
            _extraSshArgs ?? "",
            BaseBuilder.Build()
        );
    }

    public ISshCommandRequestBuilder UsingName(string nameOrId)
    {
        _nameOrId = nameOrId;
        return this;
    }

    public ISshCommandRequestBuilder UsingCommand(string command)
    {
        _command = command;
        return this;
    }

    public ISshCommandRequestBuilder WithPlain(bool with)
    {
        _plain = with;
        return this;
    }

    public ISshCommandRequestBuilder UsingExtraSshArgs(string extra)
    {
        _extraSshArgs = extra;
        return this;
    }
}