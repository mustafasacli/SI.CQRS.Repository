namespace SI.Query.Core
{
    /// <summary>
    /// Defines the <see cref="IQuery{TResult}"/>.
    /// </summary>
    /// <typeparam name="TResult">.</typeparam>
    public interface IQuery<TResult>
        where TResult : class, IQueryResult
    {
    }
}