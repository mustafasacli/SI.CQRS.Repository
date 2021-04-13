using SI.Query.Core;
using SI.QueryHandler.Core;
using System;
using System.Collections.Concurrent;

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
            var commandHandlerType = queryHandlerRegs[typeof(TQuery).FullName];
            var instance = queryHandlerInstances.GetOrAdd(commandHandlerType,
                (q) =>
                {
                    var ins = Activator.CreateInstance(q);
                    return ins;
                });
            return instance as IQueryHandler<TQuery, TQueryResult>;
        }
    }
}