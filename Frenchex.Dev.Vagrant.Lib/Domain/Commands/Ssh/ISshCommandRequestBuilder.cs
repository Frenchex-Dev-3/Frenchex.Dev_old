namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Ssh
{

    public interface ISshCommandRequestBuilder : Root.IRootCommandRequestBuilder
    {
        ISshCommandRequest Build();
        ISshCommandRequestBuilder UsingName(string nameOrId);
        ISshCommandRequestBuilder UsingCommand(string command);
        ISshCommandRequestBuilder WithPlain(bool with);
        ISshCommandRequestBuilder UsingExtraSshArgs(string extraSshArg);
    }

}
