﻿using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Halt
{
    public class HaltCommandResponseBuilderFactory : RootResponseBuilderFactory, IHaltCommandResponseBuilderFactory
    {
        public IHaltCommandResponseBuilder Build()
        {
            return new HaltCommandResponseBuilder();
        }
    }
}
