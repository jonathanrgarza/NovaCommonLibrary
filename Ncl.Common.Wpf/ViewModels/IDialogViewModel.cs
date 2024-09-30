namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// Interface for a dialog view model. It is used to get the dialog result.
/// </summary>
public interface IDialogViewModel
{
    /// <summary>
    /// Gets or sets the dialog result.
    /// </summary>
    bool? DialogResult { get; set; }
}