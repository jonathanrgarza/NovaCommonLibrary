using System;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Interface for a dialog service that can show dialogs.
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Shows a dialog with the specified view model as the data context.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="dataContext">The data context for the dialog.</param>
    /// <returns>The result of the dialog.</returns>
    /// <exception cref="InvalidOperationException">If no dialog is registered for the given <typeparam name="TViewModel" /></exception>
    bool? ShowDialog<TViewModel>(TViewModel dataContext) where TViewModel : class;

    /// <summary>
    /// Checks if a view model is registered for showing a dialog.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <returns>True if a dialog is registered, otherwise, false.</returns>
    bool IsRegistered<TViewModel>() where TViewModel : class;
}