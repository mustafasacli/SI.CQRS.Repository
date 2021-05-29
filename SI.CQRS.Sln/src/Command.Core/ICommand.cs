namespace SI.Command.Core
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TCommandResult">The type of the command result.</typeparam>
    public interface ICommand<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
    }
}