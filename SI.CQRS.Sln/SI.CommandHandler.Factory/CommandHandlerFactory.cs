using SI.Command.Core;
using SI.CommandHandler.Core;
using System;
using System.Collections.Concurrent;

namespace SI.CommandHandler.Factory
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommandHandlerFactory
    {
        private static ConcurrentDictionary<string, Type> commandHandlerRegs =
            new ConcurrentDictionary<string, Type>();

        private static ConcurrentDictionary<Type, object> commandHandlerInstances =
            new ConcurrentDictionary<Type, object>();

        static CommandHandlerFactory()
        {
            // TODO: Assembly Loading should be added.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <typeparam name="TCommandResult"></typeparam>
        /// <returns></returns>
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