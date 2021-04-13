namespace SI.QueryHandler.Core
{
    using SI.Query.Core;

    /// <summary>
    /// Defines the <see cref="IQueryHandler{TQuery, TResult}" />.
    /// </summary>
    /// <typeparam name="TQuery">.</typeparam>
    /// <typeparam name="TResult">.</typeparam>
    public interface IQueryHandler<TQuery, TResult>
                where TQuery : class, IQuery<TResult>
            where TResult : class, IQueryResult
    {
        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="query">The query<see cref="TQuery"/>.</param>
        /// <returns>The <see cref="TResult"/>.</returns>
        TResult Handle(TQuery query);
    }
}
