using Mst.DexterCfg.Factory;
using SI.Command.Core;
using SI.CommandHandler.Core;
using SimpleInfra.Common.Response;
using System.Data;

namespace SI.CommandHandler.Base
{
    /// <summary>
    /// Defines the <see cref="BaseCommandHandler{TCommand, TCommandResult}"/>.
    /// </summary>
    /// <typeparam name="TCommand">.</typeparam>
    /// <typeparam name="TCommandResult">.</typeparam>
    public abstract class BaseCommandHandler<TCommand, TCommandResult>
        : ICommandHandler<TCommand, TCommandResult>
        where TCommand : class, ICommand<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
        /// <summary>
        /// Handle Command.
        /// </summary>
        /// <param name="command">.</param>
        /// <returns>.</returns>
        public abstract SimpleResponse<TCommandResult> Handle(TCommand command);

        /// <summary>
        /// Validate command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public abstract SimpleResponse Validate(TCommand command);

        /// <summary>
        /// Authorize command.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual SimpleResponse Authorize(TCommand command)
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
            //IDbConnection conn = dbProviderFactory.CreateConnection();
            //Npgsql
            var connTypeKey = DxCfgConnectionFactory.Instance["connTypeName"];
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
    }
}