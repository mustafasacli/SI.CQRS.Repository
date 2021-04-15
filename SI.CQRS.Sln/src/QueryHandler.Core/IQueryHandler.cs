using SI.Query.Core;
using SimpleInfra.Common.Response;

namespace SI.QueryHandler.Core
{
    /// <summary>
    /// Defines the <see cref="IQueryHandler{TQuery, TResult}"/>.
    /// </summary>
    /// <typeparam name="TQuery">.</typeparam>
    /// <typeparam name="TResult">.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
                where TQuery : class, IQuery<TResult>
            where TResult : class, IQueryResult
    {
        /// <summary>
        /// Handle query.
        /// </summary>
        /// <param name="query">The query <see cref="TQuery"/>.</param>
        /// <returns>The <see cref="TResult"/>.</returns>
        SimpleResponse<TResult> Handle(TQuery query);

        /// <summary>
        /// Authorize query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        SimpleResponse Authorize(TQuery query);
    }
}