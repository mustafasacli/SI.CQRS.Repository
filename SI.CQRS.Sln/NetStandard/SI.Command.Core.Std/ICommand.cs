namespace SI.Command.Core.Std
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
