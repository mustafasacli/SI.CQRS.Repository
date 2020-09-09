using SI.Query.Core;

namespace SI.QueryBus.Core
{
    public interface IQueryBus
    {
        TQueryResult Send<TQueryResult>(IQuery<TQueryResult> query)
            where TQueryResult : class, IQueryResult;
    }
}