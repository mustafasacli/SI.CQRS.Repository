﻿using SI.Query.Core;
using SimpleInfra.Common.Response;

namespace SI.QueryBus.Core
{
    /// <summary>
    /// Defines the <see cref="IQueryBus"/>.
    /// </summary>
    public interface IQueryBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TQuery">.</typeparam>
        /// <typeparam name="TQueryResult">.</typeparam>
        /// <param name="query">The query <see cref="TQuery"/>.</param>
        /// <returns>The <see cref="TQueryResult"/>.</returns>
        SimpleResponse<TQueryResult> Send<TQuery, TQueryResult>(TQuery query)
            where TQueryResult : class, IQueryResult
            where TQuery : class, IQuery<TQueryResult>;
    }
}