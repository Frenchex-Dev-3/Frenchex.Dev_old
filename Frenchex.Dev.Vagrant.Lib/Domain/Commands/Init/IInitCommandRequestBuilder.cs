namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init
{
    public interface IInitCommandRequestBuilder : Root.IRootCommandRequestBuilder
    {
        IInitCommandRequest Build();
        IInitCommandRequestBuilder UsingBoxName(string with);
        IInitCommandRequestBuilder UsingBoxUrl(string with);
        IInitCommandRequestBuilder UsingBoxVersion(string with);
        IInitCommandRequestBuilder WithForce(bool with);
        IInitCommandRequestBuilder WithMinimal(bool with);
        IInitCommandRequestBuilder UsingOutputToFile(string with);
        IInitCommandRequestBuilder UsingTemplateFile(string with);
    }
}
