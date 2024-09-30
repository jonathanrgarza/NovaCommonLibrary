using System.Windows;
using Ncl.Common.Core.UI;

namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// Represents a view model for a message box dialog.
/// </summary>
public sealed class MessageBoxDialogViewModel : ViewModelBase, IDialogViewModel
{
    private MessageBoxResult _result;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageBoxDialogViewModel"/> class.
    /// </summary>
    /// <param name="text">The text to display in the message box.</param>
    /// <param name="caption">The caption to display in the title bar of the message box.</param>
    /// <param name="button">The button or buttons to display in the message box.</param>
    /// <param name="icon">The icon to display in the message box.</param>
    /// <param name="defaultResult">The default result of the message box.</param>
    /// <param name="options">The options for the message box.</param>
    public MessageBoxDialogViewModel(string text = "", string caption = "",
        MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None,
        MessageBoxResult defaultResult = MessageBoxResult.None,
        MessageBoxOptions options = MessageBoxOptions.None)
    {
        Text = text;
        Caption = caption;
        Button = button;
        Icon = icon;
        DefaultResult = defaultResult;
        Options = options;
    }

    /// <summary>
    /// Gets or sets the text to display in the message box.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Gets or sets the caption to display in the title bar of the message box.
    /// </summary>
    public string Caption { get; set; }

    /// <summary>
    /// Gets or sets the button or buttons to display in the message box.
    /// </summary>
    public MessageBoxButton Button { get; set; }

    /// <summary>
    /// Gets or sets the icon to display in the message box.
    /// </summary>
    public MessageBoxImage Icon { get; set; }

    /// <summary>
    /// Gets or sets the default result of the message box.
    /// </summary>
    public MessageBoxResult DefaultResult { get; set; }

    /// <summary>
    /// Gets or sets the options for the message box.
    /// </summary>
    public MessageBoxOptions Options { get; set; }

    /// <summary>
    /// Gets or sets the result of the message box.
    /// </summary>
    public MessageBoxResult Result
    {
        get => _result;
        set
        {
            if (value == _result) return;
            _result = value;
            OnPropertyChanged();
        }
    }

    /// <inheritdoc />
    public bool? DialogResult { get; set; }

    /// <summary>
    /// Creates a message box dialog for displaying an error message.
    /// </summary>
    /// <param name="message">The error message to display.</param>
    /// <param name="caption">The caption to display in the title bar of the message box.</param>
    /// <param name="button">The button or buttons to display in the message box.</param>
    /// <returns>A new instance of the <see cref="MessageBoxDialogViewModel"/> class representing the error dialog.</returns>
    public static MessageBoxDialogViewModel CreateErrorDialog(string message, string caption = "Error",
        MessageBoxButton button = MessageBoxButton.OK)
    {
        return new MessageBoxDialogViewModel(message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
    }

    /// <summary>
    /// Creates a message box dialog for displaying a warning message.
    /// </summary>
    /// <param name="message">The warning message to display.</param>
    /// <param name="caption">The caption to display in the title bar of the message box.</param>
    /// <param name="button">The button or buttons to display in the message box.</param>
    /// <returns>A new instance of the <see cref="MessageBoxDialogViewModel"/> class representing the warning dialog.</returns>
    public static MessageBoxDialogViewModel CreateWarningDialog(string message, string caption = "Warning",
        MessageBoxButton button = MessageBoxButton.OK)
    {
        return new MessageBoxDialogViewModel(message, caption, MessageBoxButton.OK, MessageBoxImage.Warning);
    }

    /// <summary>
    /// Creates a message box dialog for displaying an information message.
    /// </summary>
    /// <param name="message">The information message to display.</param>
    /// <param name="caption">The caption to display in the title bar of the message box.</param>
    /// <param name="button">The button or buttons to display in the message box.</param>
    /// <returns>A new instance of the <see cref="MessageBoxDialogViewModel"/> class representing the information dialog.</returns>
    public static MessageBoxDialogViewModel CreateInfoDialog(string message, string caption = "Information",
        MessageBoxButton button = MessageBoxButton.OK)
    {
        return new MessageBoxDialogViewModel(message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
    }

    /// <summary>
    /// Resets the values to their default values.
    /// </summary>
    public void Reset()
    {
        Text = string.Empty;
        Caption = string.Empty;
        Button = MessageBoxButton.OK;
        Icon = MessageBoxImage.None;
        DefaultResult = MessageBoxResult.None;
        Options = MessageBoxOptions.None;
        Result = MessageBoxResult.None;
    }
}