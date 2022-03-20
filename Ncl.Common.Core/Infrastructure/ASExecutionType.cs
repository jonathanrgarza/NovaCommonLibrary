namespace Ncl.Common.Core.Infrastructure
{
    /// <summary>
    ///     The execution type of the current action, for use with an Action Service (AS).
    /// </summary>
    public enum ASExecutionType
    {
        Original,
        Undo,
        Redo
    }
}