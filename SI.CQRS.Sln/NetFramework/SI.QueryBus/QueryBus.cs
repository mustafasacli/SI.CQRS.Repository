namespace SI.QueryBus
{
    using SI.Query.Core;
    using SI.QueryBus.Core;
    using SI.QueryHandler.Factory;
    using System;

    /// <summary>
    /// Defines the <see cref="QueryBus" />.
    /// </summary>
    public class QueryBus : IQueryBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TQuery">.</typeparam>
        /// <typeparam name="TQueryResult">.</typeparam>
        /// <param name="query">The query<see cref="TQuery"/>.</param>
        /// <returns>The <see cref="TQueryResult"/>.</returns>
        public TQueryResult Send<TQuery, TQueryResult>(TQuery query)
             where TQueryResult : class, IQueryResult
            where TQuery : class, IQuery<TQueryResult>
        {
            var handler =
                QueryHandlerFactory.GetQueryHandler<TQuery, TQueryResult>();
            var queryResult = handler.Handle(query);
            return queryResult;
            throw new NotImplementedException();
        }
    }
}
