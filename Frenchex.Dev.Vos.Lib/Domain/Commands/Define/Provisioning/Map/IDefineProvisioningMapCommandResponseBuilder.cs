﻿using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Provisioning.Map
{
    public interface IDefineProvisioningMapCommandResponseBuilder : IRootResponseBuilder
    {
        IDefineProvisioningMapCommandResponse Build();
    }
}
