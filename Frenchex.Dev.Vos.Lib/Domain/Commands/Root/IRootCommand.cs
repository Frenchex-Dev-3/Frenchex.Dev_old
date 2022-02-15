namespace Frenchex.Dev.Vos.Lib.Domain.Commands.Root;

public interface IRootCommand<in TU, TR>
    where TU : IRootCommandRequest
    where TR : IRootCommandResponse
{
    Task<TR> Execute(TU request);
}