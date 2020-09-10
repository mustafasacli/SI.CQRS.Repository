namespace SI.CommandBus.Core.Std
{
    using SI.Command.Core.Std;
    using SimpleInfra.Common.Response;

    /// <summary>
    /// Defines the <see cref="ICommandBus" />.
    /// </summary>
    public interface ICommandBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TCommand">.</typeparam>
        /// <typeparam name="TCommandResult">.</typeparam>
        /// <param name="command">The command<see cref="TCommand"/>.</param>
        /// <returns>The <see cref="SimpleResponse{TCommandResult}"/>.</returns>
        SimpleResponse<TCommandResult> Send<TCommand, TCommandResult>
            (TCommand command)
            where TCommand : class, ICommand<TCommandResult>
            where TCommandResult : class, ICommandResult;
    }
}
