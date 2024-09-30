using System.Windows;
using Microsoft.Win32;
using Ncl.Common.Core.Collections;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Interface for a window manager that can show windows, dialogs and manages the open windows.
/// </summary>
public interface IWindowManager
{
    /// <summary>
    /// Gets the collection of open windows.
    /// </summary>
    public IReadOnlyCollectionWrapper<Window> OpenWindows { get; }

    /// <summary>
    /// Gets the active window.
    /// </summary>
    public Window? ActiveWindow { get; }

    /// <summary>
    /// Shows a window from a view model.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="T">The type of the window.</typeparam>
    /// <param name="dataContext">The data context for the view model.</param>
    /// <param name="assignParent">Specifies whether to assign the parent window.</param>
    void ShowWindowFromViewModel<TViewModel, T>(TViewModel dataContext, bool assignParent = false)
        where TViewModel : class;

    /// <summary>
    /// Shows a window.
    /// </summary>
    /// <param name="window">The window to show.</param>
    /// <param name="assignParent">Specifies whether to assign the parent window.</param>
    void ShowWindow(Window window, bool assignParent = false);

    /// <summary>
    /// Shows a dialog window.
    /// </summary>
    /// <param name="window">The dialog window to show.</param>
    /// <param name="assignParent">Specifies whether to assign the parent window.</param>
    /// <returns>The nullable boolean result of the dialog window.</returns>
    bool? ShowDialog(Window window, bool assignParent = false);

    /// <summary>
    /// Shows a common dialog.
    /// </summary>
    /// <param name="dialog">The common dialog to show.</param>
    /// <param name="assignParent">Specifies whether to assign the parent window.</param>
    /// <returns>The nullable boolean result of the common dialog.</returns>
    bool? ShowDialog(CommonDialog dialog, bool assignParent = false);
}
