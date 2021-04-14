using SI.Command.Core;
using SI.CommandBus.Core;
using SI.CommandHandler.Factory;
using SimpleInfra.Common.Response;

namespace SI.CommandBus
{
    /// <summary>
    /// Defines the <see cref="CommandBus"/>.
    /// </summary>
    public class CommandBus : ICommandBus
    {
        /// <summary>
        /// The Send.
        /// </summary>
        /// <typeparam name="TCommand">.</typeparam>
        /// <typeparam name="TCommandResult">.</typeparam>
        /// <param name="command">.</param>
        /// <returns>.</returns>
        public SimpleResponse<TCommandResult> Send<TCommand, TCommandResult>
            (TCommand command)
            where TCommand : class, ICommand<TCommandResult>
            where TCommandResult : class, ICommandResult, new()
        {
            var handler =
                CommandHandlerFactory.GetCommandHandler<TCommand, TCommandResult>();

            var valid = handler.Validate(command);
            if (valid.ResponseCode < 0)
            {
                return new SimpleResponse<TCommandResult>
                {
                    ResponseCode = valid.ResponseCode,
                    ResponseMessage = valid.ResponseMessage,
                    RCode = valid.RCode
                };
            }

            var authorize = handler.Authorize(command);
            if (authorize.ResponseCode < 0)
            {
                return new SimpleResponse<TCommandResult>
                {
                    ResponseCode = authorize.ResponseCode,
                    ResponseMessage = authorize.ResponseMessage,
                    RCode = authorize.RCode
                };
            }
            // Get Handle method custom attributes for Parameter Validation.
            // (handler.GetType().GetMethod("Handle").GetCustomAttributes(true)?? new object[0])
            // handler.GetType().GetMethod("Handle").GetParameters()
            // command.Validate()
            /*
              var validationResult = command.Validate();
                if (validationResult.HasError)
                {
                    response.Data = new TCommandResult();
                    response.ResponseCode = -200;
#if DEBUG
                    response.ResponseMessage = validationResult.AllDevValidationMessages;
#else
                    response.ResponseMessage = validationResult.AllValidationMessages;
#endif

                    return response;
                }
             */
            var commandResult = handler.Handle(command);
            return commandResult;
        }
    }
}