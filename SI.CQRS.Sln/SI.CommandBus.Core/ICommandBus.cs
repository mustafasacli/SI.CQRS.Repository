namespace SI.CommandBus.Core
{
    using SimpleInfra.Common.Response;
    using SI.Command.Core;

    public interface ICommandBus
    {
        SimpleResponse<TCommandResult> Send<TCommand, TCommandResult>
            (TCommand command)
            where TCommand : class, ICommand<TCommandResult>
            where TCommandResult : class, ICommandResult;
    }
}