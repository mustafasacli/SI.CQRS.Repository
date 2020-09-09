namespace SI.QueryBus
{
    using SI.QueryBus.Core;
    using SI.Query.Core;
    using System;

    public class QueryBus : IQueryBus
    {
        public TQueryResult Send<TQueryResult>(IQuery<TQueryResult> query)
             where TQueryResult : class, IQueryResult
        {
            throw new NotImplementedException();
        }
    }
}
