namespace SI.QueryHandler.Factory
{
    using SI.Query.Core;
    using SI.QueryHandler.Core;
    using System;
    using System.Collections.Concurrent;

    /// <summary>
    /// 
    /// </summary>
    public class QueryHandlerFactory
    {
        private static ConcurrentDictionary<string, Type> queryHandlerRegs =
            new ConcurrentDictionary<string, Type>();

        private static ConcurrentDictionary<Type, object> queryHandlerInstances =
            new ConcurrentDictionary<Type, object>();

        static QueryHandlerFactory()
        {
            // TODO: Assembly Loading should be added.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TQuery"></typeparam>
        /// <typeparam name="TQueryResult"></typeparam>
        /// <returns></returns>
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
