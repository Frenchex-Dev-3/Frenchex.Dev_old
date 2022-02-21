﻿using System.CommandLine;

namespace Frenchex.Dev.Vos.Cli.Integration.Lib.Domain.Options;

public interface IEnabled3dOptionBuilder
{
    Option<bool> Build();
}

public class Enabled3dOptionBuilder : IEnabled3dOptionBuilder
{
    public Option<bool> Build()
    {
        return new(new[] {"--enabled", "-e"}, "Enable Machine Type");
    }
}