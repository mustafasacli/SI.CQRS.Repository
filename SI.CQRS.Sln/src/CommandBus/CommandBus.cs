﻿using SI.Command.Core;
using SI.CommandBus.Core;
using SI.CommandHandler.Factory;
using SimpleInfra.Common.Response;

namespace SI.CommandBus
{
    /// <summary>
    /// Defines the <see cref="CommandBus"/>.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TCommand">.</typeparam>
        /// <typeparam name="TCommandResult">.</typeparam>
        /// <param name="command">.</param>
        /// <returns>.</returns>
        public SimpleResponse<TCommandResult> Send<TCommand, TCommandResult>
            (TCommand command)
            where TCommand : class, ICommand<TCommandResult>
            where TCommandResult : class, ICommandResult
        {
            var handler =
                CommandHandlerFactory.GetCommandHandler<TCommand, TCommandResult>();
            var cmdResult = handler.Handle(command);
            return cmdResult;
        }
    }
}