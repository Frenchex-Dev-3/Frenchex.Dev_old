﻿namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

public abstract class RootCommandRequestBuilder : IRootCommandRequestBuilder
{
    public RootCommandRequestBuilder(
        IBaseRequestBuilderFactory baseRequestBuilderFactory
    )
    {
        BaseBuilder = baseRequestBuilderFactory.Factory(this);
    }

    public IBaseRequestBuilder BaseBuilder { get; }
}