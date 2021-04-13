namespace SI.CommandHandler.Factory
{
    using SI.Command.Core;
    using SI.CommandHandler.Core;
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
            BootstrapHandlers();
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

        private static void BootstrapHandlers()
        {
        }

        /*
        // method usage:
            BootstrapSimpleInjector();


        /// <summary>
        /// ISimpleLogger instance.
        /// </summary>
        protected ISimpleLogger Logger
        {
            get
            {
                var _logger = SimpleFileLogger.Instance;
                _logger.LogDateFormatType = SimpleLogDateFormats.Hour;
                return _logger;
            }
        }


        protected void BootstrapSimpleInjector()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();//new AsyncScopedLifestyle();

            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                if (Directory.Exists(path + "bin\\"))
                    path += "bin\\";

                Logger?.Debug($"Domain Path: {path}");
                var businessFiles = Directory.GetFiles(path, "*.Command.dll") ?? new string[] { };

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

                        RegisterAssembly(container, assembly);

                        Logger?.Info($"\"{assembly.FullName}\" is loaded.");
                        // throw new Exception("Sample Exception");
                    }
                    catch (Exception ex)
                    { Logger?.Error(ex, $"File Name: {file}"); }
                }
            }
            catch (Exception ex2)
            { Logger?.Error(ex2); }

            //container.Verify();
            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
        private void RegisterAssembly(Container container, Assembly assembly)
        {
            var registrations = assembly
                .GetExportedTypes()
                .Where(type => type.IsClass && !type.IsAbstract
                && type.Namespace.Contains(".Command.")
                //&& type.Namespace.Contains(".Business.Interfaces.") == false
                )
                .Select(q => new { service = (q.GetInterfaces() ?? new Type[0]).LastOrDefault(), implementation = q })
                .ToList();

            if (registrations == null || registrations.Count < 1)
                return;

            //var registrations =
            //    from type in assembly.GetExportedTypes()
            //    where type.IsClass && !type.IsAbstract && type.Namespace.EndsWith(".Business")
            //    from service in type.GetInterfaces()
            //    select new { service, implementation = type };

            foreach (var reg in registrations)
            {
                if (reg.service != null)
                    container.Register(reg.service, reg.implementation, Lifestyle.Scoped);
            }
        }
         */
    }
}
