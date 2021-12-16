using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Ssh
{
    public class SshCommandRequestBuilder : RootCommandRequestBuilder, ISshCommandRequestBuilder
    {
        private string? _name;
        private string? _command;
        private bool? _plain;
        private string? _extraSshArgs;

        public SshCommandRequestBuilder(IBaseRequestBuilderFactory baseRequestBuilderFactory) : base(baseRequestBuilderFactory)
        {
        }

        public ISshCommandRequest Build()
        {
            return new SshCommandRequest(
               _name ?? "",
               _command ?? "",
               _plain ?? false,
               _extraSshArgs ?? "",
               BaseBuilder.Build()
           );
        }

        public ISshCommandRequestBuilder UsingName(string nameOrId)
        {
            _name = nameOrId;
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
}
