﻿using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Up;

public interface IUpCommandResponseBuilder : IRootCommandResponseBuilder
{
    IUpCommandResponse Build();
}