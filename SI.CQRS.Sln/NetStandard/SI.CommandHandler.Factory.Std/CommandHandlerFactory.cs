namespace SI.CommandHandler.Factory.Std
{
    using SI.Command.Core.Std;
    using SI.CommandHandler.Core.Std;
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// Defines the <see cref="CommandHandlerFactory" />.
    /// </summary>
    public static class CommandHandlerFactory
    {
        /// <summary>
        /// Defines the commandHandlerRegs.
        /// </summary>
        private static ConcurrentDictionary<string, Type> commandHandlerRegs =
            new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// Defines the commandHandlerInstances.
        /// </summary>
        private static ConcurrentDictionary<Type, object> commandHandlerInstances =
            new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Initializes static members of the <see cref="CommandHandlerFactory"/> class.
        /// </summary>
        static CommandHandlerFactory()
        {
        }

        /// <summary>
        /// The GetCommandHandler.
        /// </summary>
        /// <typeparam name="TCommand">.</typeparam>
        /// <typeparam name="TCommandResult">.</typeparam>
        /// <returns>.</returns>
        public static ICommandHandler<TCommand, TCommandResult> GetCommandHandler<TCommand, TCommandResult>()
        where TCommand : class, ICommand<TCommandResult>
        where TCommandResult : class, ICommandResult
        {
            var commandHandlerType = commandHandlerRegs[typeof(TCommand).FullName];
            var instance = commandHandlerInstances.GetOrAdd(commandHandlerType,
                (q) =>
                {
                    var ins = Activator.CreateInstance(q);
                    return ins;
                });
            return instance as ICommandHandler<TCommand, TCommandResult>;
        }
    }
}
