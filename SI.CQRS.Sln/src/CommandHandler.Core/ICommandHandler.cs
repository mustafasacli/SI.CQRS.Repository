using SI.Command.Core;
using SimpleInfra.Common.Response;

namespace SI.CommandHandler.Core
{
    /// <summary>
    /// Defines the <see cref="ICommandHandler{TCommand, TCommandResult}"/>.
    /// </summary>
    /// <typeparam name="TCommand">.</typeparam>
    /// <typeparam name="TCommandResult">.</typeparam>
    public interface ICommandHandler<TCommand, TCommandResult>
        where TCommand : class, ICommand<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="command">.</param>
        /// <returns>.</returns>
        SimpleResponse<TCommandResult> Handle(TCommand command);

        /// <summary>
        /// Validate command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        SimpleResponse Validate(TCommand command);

        /// <summary>
        /// Authorize command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
       SimpleResponse Authorize(TCommand command);
    }
}