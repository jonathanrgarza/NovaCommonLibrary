using System.Windows;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
///     Interface for a <see cref="MessageBox" /> helper.
/// </summary>
public interface IMessageBoxHelper
{
    /// <summary>
    ///     Gets/Sets the owner window for any dialog shown.
    /// </summary>
    Window? Owner { get; set; }

    /// <summary>
    ///     Displays a message box with specified text, caption, and style.
    /// </summary>
    /// <remarks>
    ///     Uses the <see cref="Owner" /> window unless its <see langword="null" />, which then will use currently active
    ///     window.
    /// </remarks>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: No caption.</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: No icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: No default result.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult Show(
        string messageBoxText,
        string caption = "",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.None,
        MessageBoxResult defaultResult = MessageBoxResult.None,
        MessageBoxOptions options = MessageBoxOptions.None);

    /// <summary>
    ///     Displays a message box with specified text, caption, and style.
    /// </summary>
    /// <param name="window">
    ///     The parent window for the dialog.
    ///     If <see langword="null" />, will use the currently active window instead.
    /// </param>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: No caption.</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: No icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: No default result.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult Show(Window? window,
        string messageBoxText,
        string caption = "",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.None,
        MessageBoxResult defaultResult = MessageBoxResult.None,
        MessageBoxOptions options = MessageBoxOptions.None);

    /// <summary>
    ///     Displays an error message box with specified text, caption, and style.
    /// </summary>
    /// <remarks>
    ///     Uses the <see cref="Owner" /> window unless its <see langword="null" />, which then will use currently active
    ///     window.
    /// </remarks>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: Error</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: An error icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: OK button default.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult ShowError(
        string messageBoxText,
        string caption = "Error",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.Error,
        MessageBoxResult defaultResult = MessageBoxResult.OK,
        MessageBoxOptions options = MessageBoxOptions.None);

    /// <summary>
    ///     Displays an error message box with specified text, caption, and style.
    /// </summary>
    /// <param name="window">
    ///     The parent window for the dialog.
    ///     If <see langword="null" />, will use the currently active window instead.
    /// </param>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: Error</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: An error icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: OK button default.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult ShowError(Window? window,
        string messageBoxText,
        string caption = "Error",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.Error,
        MessageBoxResult defaultResult = MessageBoxResult.OK,
        MessageBoxOptions options = MessageBoxOptions.None);

    /// <summary>
    ///     Displays a warning message box with specified text, caption, and style.
    /// </summary>
    /// <remarks>
    ///     Uses the <see cref="Owner" /> window unless its <see langword="null" />, which then will use currently active
    ///     window.
    /// </remarks>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: Warning</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: A warning icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: OK button default.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult ShowWarning(
        string messageBoxText,
        string caption = "Warning",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.Warning,
        MessageBoxResult defaultResult = MessageBoxResult.OK,
        MessageBoxOptions options = MessageBoxOptions.None);

    /// <summary>
    ///     Displays a warning message box with specified text, caption, and style.
    /// </summary>
    /// <param name="window">
    ///     The parent window for the dialog.
    ///     If <see langword="null" />, will use the currently active window instead.
    /// </param>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: Warning</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: A warning icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: OK button default.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult ShowWarning(Window? window,
        string messageBoxText,
        string caption = "Warning",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.Warning,
        MessageBoxResult defaultResult = MessageBoxResult.OK,
        MessageBoxOptions options = MessageBoxOptions.None);

    /// <summary>
    ///     Displays a information message box with specified text, caption, and style.
    /// </summary>
    /// <remarks>
    ///     Uses the <see cref="Owner" /> window unless its <see langword="null" />, which then will use currently active
    ///     window.
    /// </remarks>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: Information</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: A information icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: OK button default.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult ShowInfo(
        string messageBoxText,
        string caption = "Information",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.Information,
        MessageBoxResult defaultResult = MessageBoxResult.OK,
        MessageBoxOptions options = MessageBoxOptions.None);

    /// <summary>
    ///     Displays a information message box with specified text, caption, and style.
    /// </summary>
    /// <param name="window">
    ///     The parent window for the dialog.
    ///     If <see langword="null" />, will use the currently active window instead.
    /// </param>
    /// <param name="messageBoxText">The text to show.</param>
    /// <param name="caption">The title of the dialog. Default: Information</param>
    /// <param name="button">The button or buttons to show on the dialog. Default: An OK button only.</param>
    /// <param name="icon">The icon to show on the dialog. Default: A information icon.</param>
    /// <param name="defaultResult">The default result for the dialog. Default: OK button default.</param>
    /// <param name="options">The additional options for the dialog. Default: No additional options.</param>
    /// <returns>A <see cref="MessageBoxResult" /> which specifies which button was clicked by the user.</returns>
    MessageBoxResult ShowInfo(Window? window,
        string messageBoxText,
        string caption = "Information",
        MessageBoxButton button = MessageBoxButton.OK,
        MessageBoxImage icon = MessageBoxImage.Information,
        MessageBoxResult defaultResult = MessageBoxResult.OK,
        MessageBoxOptions options = MessageBoxOptions.None);
}