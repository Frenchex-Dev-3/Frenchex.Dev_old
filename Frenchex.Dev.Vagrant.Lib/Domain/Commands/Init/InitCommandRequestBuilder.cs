using Frenchex.Dev.Vagrant.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vagrant.Lib.Domain.Commands.Init;

public class InitCommandRequestBuilder : RootCommandRequestBuilder, IInitCommandRequestBuilder
{
    private string? _boxName;
    private string? _boxUrl;
    private string? _boxVersion;
    private bool? _force;
    private bool? _minimal;
    private string? _outputToFile;
    private string? _templateFile;

    public InitCommandRequestBuilder(
        IBaseCommandRequestBuilderFactory baseRequestBuilderFactory
    ) : base(baseRequestBuilderFactory)
    {
    }

    public IInitCommandRequest Build()
    {
        return new InitCommandRequest(
            _boxVersion,
            _force,
            _minimal,
            _outputToFile,
            _templateFile,
            _boxName,
            _boxUrl,
            BaseBuilder.Build()
        );
    }

    public IInitCommandRequestBuilder UsingBoxVersion(string boxVersion)
    {
        _boxVersion = boxVersion;
        return this;
    }

    public IInitCommandRequestBuilder WithForce(bool with)
    {
        _force = with;
        return this;
    }

    public IInitCommandRequestBuilder WithMinimal(bool with)
    {
        _minimal = with;
        return this;
    }

    public IInitCommandRequestBuilder UsingOutputToFile(string file)
    {
        _outputToFile = file;
        return this;
    }

    public IInitCommandRequestBuilder UsingTemplateFile(string templateFile)
    {
        _templateFile = templateFile;
        return this;
    }

    public IInitCommandRequestBuilder UsingBoxName(string boxName)
    {
        _boxName = boxName;
        return this;
    }

    public IInitCommandRequestBuilder UsingBoxUrl(string boxUrl)
    {
        _boxUrl = boxUrl;
        return this;
    }
}