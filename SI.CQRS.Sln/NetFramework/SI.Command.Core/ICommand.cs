namespace SI.Command.Core
{
    /// <summary>
    /// Defines the <see cref="ICommand{TCommandResult}" />.
    /// </summary>
    /// <typeparam name="TCommandResult">.</typeparam>
    public interface ICommand<TCommandResult>
        where TCommandResult : class, ICommandResult
    {
    }
}
