using SI.Command.Core;
//using SI.CommandHandler.Base;
using SI.CommandHandler.Core;
using SimpleFileLogging;
using SimpleFileLogging.Enums;
using SimpleFileLogging.Interfaces;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SI.CommandHandler.Factory
{
    /// <summary>
    /// Defines the <see cref="CommandHandlerFactory"/>.
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
            BootstrapHandlers();
        }

        /// <summary>
        /// Gets CommandHandler instance.
        /// </summary>
        /// <typeparam name="TCommand">.</typeparam>
        /// <typeparam name="TCommandResult">.</typeparam>
        /// <returns>.</returns>
        public static ICommandHandler<TCommand, TCommandResult> GetCommandHandler<TCommand, TCommandResult>()
        where TCommand : class, ICommand<TCommandResult>
        where TCommandResult : class, ICommandResult, new()
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

        /// <summary>
        /// ISimpleLogger instance.
        /// </summary>
        private static ISimpleLogger Logger
        {
            get
            {
                var _logger = SimpleFileLogger.Instance;
                _logger.LogDateFormatType = SimpleLogDateFormats.Day;
                return _logger;
            }
        }

        /// <summary>
        /// Bootstraps Command Handlers.
        /// </summary>
        private static void BootstrapHandlers()
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                if (Directory.Exists(path + "bin\\"))
                    path += "bin\\";

                Logger?.Debug($"Domain Path: {path}");
                var businessFiles = Directory.GetFiles(path, "*.CommandHandlers.dll") ?? new string[] { };

                foreach (var file in businessFiles)
                {
                    try
                    {
                        var assembly = Assembly.LoadFile(file);
                        var referencedAssemblies = assembly.GetReferencedAssemblies() ?? new AssemblyName[0];
                        referencedAssemblies.ToList().ForEach(a =>
                        {
                            try
                            {
                                Assembly.Load(a);
                            }
                            catch (Exception e2)
                            {
                                Logger?.Error(e2, $"Asemmbly could not be loaded. Assembly: {a.FullName}");
                                throw;
                            }
                        });

                        RegisterAssembly(assembly);

                        Logger?.Info($"\"{assembly.FullName}\" is loaded.");
                        // throw new Exception("Sample Exception");
                    }
                    catch (Exception ex)
                    { Logger?.Error(ex, $"File Name: {file}"); }
                }
            }
            catch (Exception ex2)
            { Logger?.Error(ex2); }
        }

        /// <summary>
        /// Registers CommandHandlers Assembly.
        /// </summary>
        /// <param name="assembly"></param>
        private static void RegisterAssembly(Assembly assembly)
        {
            var registrations = assembly
                .GetExportedTypes()
                .Where(type => type.IsClass && !type.IsAbstract
                && type.Namespace.Contains(".CommandHandlers.")
                && type.BaseType.IsGenericType
                // && type.BaseType.GetGenericTypeDefinition() == typeof(SI.CommandHandler.Base.BaseCommandHandler<,>)
                && type.GetInterfaces().LastOrDefault().GetGenericTypeDefinition() == typeof(ICommandHandler<,>)
                && (type.BaseType.GetGenericArguments() ?? new Type[0]).Length == 2
                )
                .Select(q => new { service = q.BaseType.GetGenericArguments()[0].FullName, implementation = q })
                .ToList();

            if (registrations == null || registrations.Count < 1)
                return;

            foreach (var reg in registrations)
            {
                commandHandlerRegs[reg.service] = reg.implementation;
            }
        }
    }
}