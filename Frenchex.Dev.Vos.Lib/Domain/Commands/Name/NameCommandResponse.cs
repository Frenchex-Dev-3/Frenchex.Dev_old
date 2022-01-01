namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public class NameCommandResponse : INameCommandResponse
{
    public NameCommandResponse(string[] names)
    {
        Names = names;
    }

    public string[] Names { get; }
}