using System;
using System.Windows;
using Microsoft.Win32;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
///  Represents a helper class for showing dialogs.
/// </summary>
public interface IDialogHelper
{
    /// <summary>
    ///     Gets/Sets the owner window for any dialog shown.
    /// </summary>
    Window? Owner { get; set; }

    /// <summary>
    ///     Shows the given <see cref="Window" /> as a dialog.
    ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
    /// </summary>
    /// <param name="dialog">The window to show as a dialog.</param>
    /// <returns>
    ///     A <see cref="Nullable{T}" /> of type <see cref="bool" /> that specifies whether the
    ///     activity was accepted (<see langword="true" />) or canceled (<see langword="false" />).
    ///     The return value is the value of <see cref="Window.DialogResult" /> property before a window closes.
    /// </returns>
    bool? ShowDialog(Window dialog);

    /// <summary>
    ///     Shows the given <see cref="CommonDialog" /> (e.g. <see cref="OpenFileDialog" />, <see cref="SaveFileDialog" />).
    ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
    /// </summary>
    /// <param name="dialog">The dialog to show.</param>
    /// <returns>
    ///     If the user clicks the OK button of the dialog being displayed, <see langword="true" /> is returned, otherwise,
    ///     <see langword="false" />.
    /// </returns>
    bool? ShowDialog(CommonDialog dialog);

    /// <summary>
    ///     Shows a <see cref="SaveFileDialog" /> and returns the user selected save file path,
    ///     or an empty <see cref="string" /> on cancel.
    ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
    /// </summary>
    /// <returns>The file path or an empty <see cref="string" /> on cancel.</returns>
    string GetSaveFilePath(string? title = null,
        string? filter = null, string? initialDirectory = null,
        bool overwritePrompt = true, bool createPrompt = false, bool checkPathExists = false,
        bool addExtension = true, bool validateNames = true,
        string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
        bool restoreDirectory = false, bool addToRecent = false, string? initialFileName = null);

    /// <summary>
    ///     Shows a <see cref="OpenFileDialog" /> and returns the user selected file path,
    ///     or an empty <see cref="string" /> on cancel.
    ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
    /// </summary>
    /// <returns>The file path or an empty <see cref="string" /> on cancel.</returns>
    string GetOpenFilePath(string? title = null, string? filter = null,
        string? initialDirectory = null,
        bool checkFileExists = false, bool readOnlyChecked = false, bool showReadOnly = false,
        bool addExtension = true, bool validateNames = true,
        string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
        bool addToRecent = false, string? initialFileName = null);

    /// <summary>
    ///     Shows a <see cref="OpenFileDialog" /> and returns the user selected file path(s),
    ///     or an empty array on cancel.
    ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
    /// </summary>
    /// <returns>The file path(s) or an empty array on cancel.</returns>
    string[] GetMultiOpenFilePaths(string? title = null, string? filter = null,
        string? initialDirectory = null,
        bool checkFileExists = false, bool readOnlyChecked = false, bool showReadOnly = false,
        bool addExtension = true, bool validateNames = true,
        string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
        bool addToRecent = false, string? initialFileName = null);

    /// <summary>
    ///    Shows a <see cref="OpenFolderDialog" /> and returns the user selected folder path,
    ///   or an empty <see cref="string" /> on cancel.
    /// </summary>
    /// <returns>The folder path or an empty <see cref="string" /> on cancel.</returns>
    string GetOpenFolderPath(string? title = null,
        string? initialDirectory = null, string? rootDirectory = null,
        bool validateNames = true, bool addToRecent = false, string? initialFolderName = null);

    /// <summary>
    ///    Shows a <see cref="OpenFolderDialog" /> and returns the user selected folder path(s),
    ///   or an empty array on cancel.
    /// </summary>
    /// <returns>The folder path(s) or an empty array on cancel.</returns>
    string[] GetMultiOpenFolderPath(string? title = null,
        string? initialDirectory = null, string? rootDirectory = null,
        bool validateNames = true, bool addToRecent = false, string? initialFolderName = null);
}