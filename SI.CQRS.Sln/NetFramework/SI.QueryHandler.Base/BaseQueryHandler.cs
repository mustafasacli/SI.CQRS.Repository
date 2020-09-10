namespace SI.QueryHandler.Base
{
    using Mst.DexterCfg.Factory;
    using SI.Query.Core;
    using SI.QueryHandler.Core;
    using SimpleFileLogging;
    using SimpleFileLogging.Enums;
    using SimpleFileLogging.Interfaces;
    using System.Data;

    /// <summary>
    /// Defines the <see cref="BaseQueryHandler{TQuery, TResult}" />.
    /// </summary>
    /// <typeparam name="TQuery">.</typeparam>
    /// <typeparam name="TResult">.</typeparam>
    public abstract class BaseQueryHandler<TQuery, TResult> : IQueryHandler<TQuery, TResult>
            where TQuery : class, IQuery<TResult>
        where TResult : class, IQueryResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseQueryHandler{TQuery, TResult}"/> class.
        /// </summary>
        protected BaseQueryHandler()
        {
        }

        /// <summary>
        /// The Handle.
        /// </summary>
        /// <param name="query">The query<see cref="TQuery"/>.</param>
        /// <returns>The <see cref="TResult"/>.</returns>
        public abstract TResult Handle(TQuery query);

        /// <summary>
        /// The GetDbConnection.
        /// </summary>
        /// <returns>.</returns>
        protected virtual IDbConnection GetDbConnection()
        {
            var connTypeKey = DxCfgConnectionFactory.Instance["connTypeName"];
            IDbConnection conn = DxCfgConnectionFactory.Instance.GetConnection(connTypeKey);
            var connstrKey = DxCfgConnectionFactory.Instance["connStringName"];
            conn.ConnectionString = DxCfgConnectionFactory.Instance[connstrKey];
            return conn;
        }

        /// <summary>
        /// Gets the DayLogger.
        /// </summary>
        protected ISimpleLogger DayLogger
        {
            get
            {
                var logger = SimpleFileLogger.Instance;
                logger.LogDateFormatType = SimpleLogDateFormats.Day;
                return logger;
            }
        }

        /// <summary>
        /// Gets the HourLogger.
        /// </summary>
        protected ISimpleLogger HourLogger
        {
            get
            {
                var logger = SimpleFileLogger.Instance;
                logger.LogDateFormatType = SimpleLogDateFormats.Hour;
                return logger;
            }
        }

        /// <summary>
        /// Gets the NoneLogger.
        /// </summary>
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
