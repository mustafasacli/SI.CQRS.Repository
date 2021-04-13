﻿namespace SI.CommandHandler.Core
{
    using SI.Command.Core;
    using SimpleInfra.Common.Response;

    /// <summary>
    /// Defines the <see cref="ICommandHandler{TCommand, TCommandResult}" />.
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
    }
}