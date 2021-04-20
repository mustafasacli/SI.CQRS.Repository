using SI.Query.Core;
using SI.QueryBus.Core;
using SI.QueryHandler.Factory;
using SimpleInfra.Common.Response;
using System;

namespace SI.QueryBus
{
    /// <summary>
    /// Defines the <see cref="QueryBus"/>.
    /// </summary>
    public class QueryBus : IQueryBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TQuery">.</typeparam>
        /// <typeparam name="TQueryResult">.</typeparam>
        /// <param name="query">The query <see cref="TQuery"/>.</param>
        /// <returns>The <see cref="TQueryResult"/>.</returns>
        public SimpleResponse<TQueryResult> Send<TQuery, TQueryResult>(TQuery query)
             where TQueryResult : class, IQueryResult
            where TQuery : class, IQuery<TQueryResult>
        {
            var handler =
                QueryHandlerFactory.GetQueryHandler<TQuery, TQueryResult>();

            var authorize = handler.Authorize(query);
            if (authorize.ResponseCode < 0)
            {
                return new SimpleResponse<TQueryResult>
                {
                    ResponseCode = authorize.ResponseCode,
                    ResponseMessage = authorize.ResponseMessage,
                    RCode = authorize.RCode
                };
            }

            var queryResult = handler.Handle(query);
            return queryResult;
        }
    }
}