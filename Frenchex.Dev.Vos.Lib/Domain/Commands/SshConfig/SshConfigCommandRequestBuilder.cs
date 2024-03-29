﻿using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.SshConfig;

public class SshConfigCommandRequestBuilder : RootCommandRequestBuilder, ISshConfigCommandRequestBuilder
{
    private string? _host;
    private string? _nameOrId;

    public SshConfigCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(
        baseRequestBuilderFactory)
    {
    }

    public ISshConfigCommandRequest Build()
    {
        return new SshConfigCommandRequest(
            _nameOrId ?? "",
            _host ?? "",
            BaseBuilder.Build()
        );
    }

    public ISshConfigCommandRequestBuilder UsingName(string nameOrId)
    {
        _nameOrId = nameOrId;
        return this;
    }

    public ISshConfigCommandRequestBuilder UsingHost(string host)
    {
        _host = host;
        return this;
    }
}