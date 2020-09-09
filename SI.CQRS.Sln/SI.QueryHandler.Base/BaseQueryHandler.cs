namespace SI.QueryHandler.Base
{
    using Mst.DexterCfg.Factory;
    using SimpleFileLogging;
    using SimpleFileLogging.Enums;
    using SimpleFileLogging.Interfaces;
    using SI.Query.Core;
    using SI.QueryHandler.Core;
    using System.Data;

    public abstract class BaseQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
            where TQuery : class, IQuery<TResult>
        where TResult : class, IQueryResult
    {
        protected BaseQueryHandler()
        {
        }

        public abstract TResult Handle(TQuery query);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        protected virtual IDbConnection GetDbConnection()
        {
            var connTypeKey = DxCfgConnectionFactory.Instance["connTypeName"];
            IDbConnection conn = DxCfgConnectionFactory.Instance.GetConnection(connTypeKey);
            var connstrKey = DxCfgConnectionFactory.Instance["connStringName"];
            conn.ConnectionString = DxCfgConnectionFactory.Instance[connstrKey];
            return conn;
        }

        protected ISimpleLogger DayLogger
        {
            get
            {
                var logger = SimpleFileLogger.Instance;
                logger.LogDateFormatType = SimpleLogDateFormats.Day;
                return logger;
            }
        }

        protected ISimpleLogger HourLogger
        {
            get
            {
                var logger = SimpleFileLogger.Instance;
                logger.LogDateFormatType = SimpleLogDateFormats.Hour;
                return logger;
            }
        }

        protected ISimpleLogger NoneLogger
        {
            get
            {
                var logger = SimpleFileLogger.Instance;
                logger.LogDateFormatType = SimpleLogDateFormats.None;
                return logger;
            }
        }
    }
}