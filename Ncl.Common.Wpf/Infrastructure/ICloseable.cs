namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Represents a closeable object/window.
/// </summary>
public interface ICloseable
{
    /// <summary>
    /// Requests the object/window to close.
    /// </summary>
    void Close();
}