using Frenchex.Dev.Vos.Lib.Domain.Bases;
using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Define.Machine.Add
{
    public class DefineMachineAddCommandRequestBuilder : IDefineMachineAddCommandRequestBuilder
    {
        public IBaseRequestBuilder BaseBuilder { get; }

        public DefineMachineAddCommandRequestBuilder(
            IBaseRequestBuilderFactory baseRequestBuilderFactory
        )
        {
            BaseBuilder = baseRequestBuilderFactory.Factory(this);
        }
        public IDefineMachineAddCommandRequest Build()
        {
            if (null == _definition)
            {
                throw new InvalidOperationException("definition is null");
            }

            return new DefineMachineAddCommandRequest(
                _definition,
                BaseBuilder.Build()
            );
        }

        private MachineDefinition? _definition;
        public IDefineMachineAddCommandRequestBuilder UsingDefinition(MachineDefinition definition)
        {
            _definition = definition;
            return this;
        }
    }
}
