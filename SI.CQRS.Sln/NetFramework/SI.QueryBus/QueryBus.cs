namespace SI.QueryBus
{
    using SI.Query.Core;
    using SI.QueryBus.Core;
    using System;

    /// <summary>
    /// Defines the <see cref="QueryBus" />.
    /// </summary>
    public class QueryBus : IQueryBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TQueryResult">.</typeparam>
        /// <param name="query">The query<see cref="IQuery{TQueryResult}"/>.</param>
        /// <returns>The <see cref="TQueryResult"/>.</returns>
        public TQueryResult Send<TQueryResult>(IQuery<TQueryResult> query)
             where TQueryResult : class, IQueryResult
        {
            throw new NotImplementedException();
        }
    }
}
