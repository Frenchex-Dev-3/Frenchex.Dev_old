using Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Name;

public interface INameCommandResponseBuilder : IRootResponseBuilder
{
    INameCommandResponseBuilder SetNames(string[] names);
    INameCommandResponse Build();
}