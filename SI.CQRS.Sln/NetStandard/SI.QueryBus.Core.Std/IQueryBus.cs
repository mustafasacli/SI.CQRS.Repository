﻿namespace SI.QueryBus.Core.Std
{
    using SI.Query.Core.Std;

    /// <summary>
    /// Defines the <see cref="IQueryBus" />.
    /// </summary>
    public interface IQueryBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TQueryResult">.</typeparam>
        /// <param name="query">The query<see cref="IQuery{TQueryResult}"/>.</param>
        /// <returns>The <see cref="TQueryResult"/>.</returns>
        TQueryResult Send<TQueryResult>(IQuery<TQueryResult> query)
            where TQueryResult : class, IQueryResult;
    }
}