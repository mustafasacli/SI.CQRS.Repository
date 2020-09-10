namespace SI.CommandHandler.Core.Std
{
    using SI.Command.Core.Std;
    using SimpleInfra.Common.Response;

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <typeparam name="TCommandResult"></typeparam>
    public interface ICommandHandler<TCommand, TCommandResult>
        where TCommand : class, ICommand<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        SimpleResponse<TCommandResult> Handle(TCommand command);
    }
}