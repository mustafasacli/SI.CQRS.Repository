using Mst.DexterCfg.Factory;
using SI.Query.Core;
using SI.QueryHandler.Core;
using SimpleFileLogging;
using SimpleFileLogging.Enums;
using SimpleFileLogging.Interfaces;
using SimpleInfra.Common.Response;
using System.Data;
using System.Data.Common;

namespace SI.QueryHandler.Base
{
    /// <summary>
    /// Defines the <see cref="BaseQueryHandler{TQuery, TResult}"/>.
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
        /// Handle query.
        /// </summary>
        /// <param name="query">The query <see cref="TQuery"/>.</param>
        /// <returns>The <see cref="TResult"/>.</returns>
        public abstract SimpleResponse<TResult> Handle(TQuery query);

        /// <summary>
        /// Authorize query.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public virtual SimpleResponse Authorize(TQuery query)
        {
            return new SimpleResponse();
        }


        /// <summary>
        /// Gets DbConnection.
        /// </summary>
        /// <returns>.</returns>
        protected virtual IDbConnection GetDbConnection()
        {
            //var dbProviderFactory = DbProviderFactories.GetFactory("Npgsql");
            //Npgsql
            var connTypeKey = DxCfgConnectionFactory.Instance["connTypeName"];
            //IDbConnection conn = dbProviderFactory.CreateConnection();
            IDbConnection conn = DxCfgConnectionFactory.Instance.GetConnection(connTypeKey);
            var connstrKey = DxCfgConnectionFactory.Instance["connStringName"];
            conn.ConnectionString = DxCfgConnectionFactory.Instance[connstrKey];
            return conn;
        }

        /// <summary>
        /// Gets DbConnection.
        /// </summary>
        /// <returns>.</returns>
        protected virtual IDbConnection GetDbConnection(string connTypeName, string connStringName)
        {
            IDbConnection conn = DxCfgConnectionFactory.Instance.GetConnection(connTypeName);
            conn.ConnectionString = DxCfgConnectionFactory.Instance[connStringName];
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