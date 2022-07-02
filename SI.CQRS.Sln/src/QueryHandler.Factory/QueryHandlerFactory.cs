using SI.Logging;
using SI.Query.Core;
using SI.QueryHandler.Core;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Reflection;

namespace SI.QueryHandler.Factory
{
    /// <summary>
    /// Defines the <see cref="QueryHandlerFactory"/>.
    /// </summary>
    public class QueryHandlerFactory
    {
        /// <summary>
        /// Defines the queryHandlerRegs.
        /// </summary>
        private static ConcurrentDictionary<string, Type> queryHandlerRegs =
            new ConcurrentDictionary<string, Type>();

        /// <summary>
        /// Defines the queryHandlerInstances.
        /// </summary>
        private static ConcurrentDictionary<Type, object> queryHandlerInstances =
            new ConcurrentDictionary<Type, object>();

        /// <summary>
        /// Initializes static members of the <see cref="QueryHandlerFactory"/> class.
        /// </summary>
        static QueryHandlerFactory()
        {
            BootstrapHandlers();
        }

        /// <summary>
        /// The GetQueryHandler.
        /// </summary>
        /// <typeparam name="TQuery">.</typeparam>
        /// <typeparam name="TQueryResult">.</typeparam>
        /// <returns>.</returns>
        public static IQueryHandler<TQuery, TQueryResult> GetQueryHandler<TQuery, TQueryResult>()
        where TQuery : class, IQuery<TQueryResult>
        where TQueryResult : class, IQueryResult
        {
            var queryHandlerType = queryHandlerRegs[typeof(TQuery).FullName];
            var instance = queryHandlerInstances.GetOrAdd(queryHandlerType,
                (q) =>
                {
                    var ins = Activator.CreateInstance(q);
                    return ins;
                });
            return instance as IQueryHandler<TQuery, TQueryResult>;
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

                SimpleCommonLogger.DayLogger?.Debug($"Domain Path: {path}");
                var businessFiles = Directory.GetFiles(path, "*.QueryHandlers.dll") ?? new string[] { };

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
                                SimpleCommonLogger.DayLogger?.Error(e2, $"Asemmbly could not be loaded. Assembly: {a.FullName}");
                                throw;
                            }
                        });

                        RegisterAssembly(assembly);

                        SimpleCommonLogger.DayLogger?.Info($"\"{assembly.FullName}\" is loaded.");
                    }
                    catch (Exception ex)
                    { SimpleCommonLogger.DayLogger?.Error(ex, $"File Name: {file}"); }
                }
            }
            catch (Exception ex2)
            { SimpleCommonLogger.DayLogger?.Error(ex2); }
        }

        /// <summary>
        /// Registers QueryHandlers Assembly.
        /// </summary>
        /// <param name="assembly"></param>
        private static void RegisterAssembly(Assembly assembly)
        {
            var registrations = assembly
                .GetExportedTypes()
                .Where(type => type.IsClass && !type.IsAbstract
                && type.Namespace.Contains(".QueryHandlers.")
                && type.BaseType.IsGenericType
                //&& type.BaseType.GetGenericTypeDefinition() == typeof(BaseQueryHandler<,>)
                && type.GetInterfaces().LastOrDefault().GetGenericTypeDefinition() == typeof(IQueryHandler<,>)
                && (type.BaseType.GetGenericArguments() ?? new Type[0]).Length == 2
                )
                .Select(q => new { service = q.BaseType.GetGenericArguments()[0].FullName, implementation = q })
                .ToList();

            if (registrations == null || registrations.Count < 1)
                return;

            foreach (var reg in registrations)
            {
                queryHandlerRegs[reg.service] = reg.implementation;
            }
        }
    }
}