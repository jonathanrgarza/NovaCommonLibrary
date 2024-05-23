namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Interface for a dialog that can be closed.
/// </summary>
public interface ICloseableDialog : ICloseable
{
    /// <summary>
    /// Closes the dialog with the specified dialog result.
    /// </summary>
    /// <param name="dialogResult">The dialog result to set.</param>
    void CloseDialog(bool? dialogResult = null);
}