namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public class NameCommandResponseBuilder : INameCommandResponseBuilder
{
    private string[]? _names;

    public INameCommandResponse Build()
    {
        if (null == _names) throw new InvalidOperationException("Names is null");

        return new NameCommandResponse(_names);
    }

    public INameCommandResponseBuilder SetNames(string[] names)
    {
        _names = names;
        return this;
    }
}