using System;
using System.Windows;
using Microsoft.Win32;

namespace Ncl.Common.Wpf.Infrastructure
{
    /// <summary>
    ///     Helper utility for dialogs, <see cref="SaveFileDialog" />, <see cref="OpenFileDialog" /> and
    ///     <see cref="OpenFolderDialog"/>.
    /// </summary>
    public class DialogHelper : IDialogHelper
    {
        private OpenFileDialog? _openFileDialog;
        private SaveFileDialog? _saveFileDialog;
        private OpenFolderDialog? _openFolderDialog;

        /// <summary>
        ///     Initializes a new instance of <see cref="DialogHelper" />.
        /// </summary>
        public DialogHelper()
        {
            //Intentionally left blank
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="DialogHelper" />.
        /// </summary>
        /// <param name="owner">The initial owner window.</param>
        public DialogHelper(Window? owner)
        {
            Owner = owner;
        }


        /// <summary>
        ///     Gets/Sets the owner window for any dialog shown.
        /// </summary>
        public Window? Owner { get; set; }

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
        public bool? ShowDialog(Window dialog)
        {
            var owner = Owner;
            if (owner == null)
                return dialog.ShowDialog();

            var previousOwner = dialog.Owner;
            try
            {
                dialog.Owner = owner;

                return dialog.ShowDialog();
            }
            finally
            {
                dialog.Owner = previousOwner;
            }
        }

        /// <summary>
        ///     Shows the given <see cref="CommonDialog" /> (e.g. <see cref="OpenFileDialog" />, <see cref="SaveFileDialog" />).
        ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
        /// </summary>
        /// <param name="dialog">The dialog to show.</param>
        /// <returns>
        ///     If the user clicks the OK button of the dialog being displayed, <see langword="true" /> is returned, otherwise,
        ///     <see langword="false" />.
        /// </returns>
        public bool? ShowDialog(CommonDialog dialog)
        {
            var owner = Owner;
            if (owner == null)
                return dialog.ShowDialog();

            return dialog.ShowDialog(owner);
        }

        /// <summary>
        ///     Shows a <see cref="SaveFileDialog" /> and returns the user selected save file path,
        ///     or an empty <see cref="string" /> on cancel.
        ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
        /// </summary>
        /// <returns>The file path or an empty <see cref="string" /> on cancel.</returns>
        public string GetSaveFilePath(string? title = null,
            string? filter = null, string? initialDirectory = null,
            bool overwritePrompt = true, bool createPrompt = false, bool checkPathExists = false,
            bool addExtension = true, bool validateNames = true,
            string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
            bool restoreDirectory = false, bool addToRecent = false, string? initialFileName = null)
        {
            _saveFileDialog ??= new SaveFileDialog();
            var dialog = _saveFileDialog;

            SetSaveFileDialogOptions(dialog, title, filter, initialDirectory,
                overwritePrompt, createPrompt, checkPathExists,
                addExtension, validateNames, defaultExt, filterIndex, dereferenceLinks,
                restoreDirectory, addToRecent, initialFileName);

            if (dialog.ShowDialog(Owner) != true)
                return string.Empty;

            return dialog.FileName;
        }

        /// <summary>
        ///     Shows a <see cref="OpenFileDialog" /> and returns the user selected file path,
        ///     or an empty <see cref="string" /> on cancel.
        ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
        /// </summary>
        /// <returns>The file path or an empty <see cref="string" /> on cancel.</returns>
        public string GetOpenFilePath(string? title = null, string? filter = null,
            string? initialDirectory = null,
            bool checkFileExists = false, bool readOnlyChecked = false, bool showReadOnly = false,
            bool addExtension = true, bool validateNames = true,
            string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
            bool addToRecent = false, string? initialFileName = null)
        {
            _openFileDialog ??= new OpenFileDialog();
            var dialog = _openFileDialog;

            SetOpenFileDialogOptions(dialog, false, title, filter, initialDirectory,
                checkFileExists, readOnlyChecked, showReadOnly, addExtension, validateNames,
                defaultExt, filterIndex, dereferenceLinks, addToRecent, initialFileName);

            if (dialog.ShowDialog(Owner) != true)
                return string.Empty;

            return dialog.FileName;
        }

        /// <summary>
        ///     Shows a <see cref="OpenFileDialog" /> and returns the user selected file path(s),
        ///     or an empty array on cancel.
        ///     Uses <see cref="Owner" />, if its not <see langword="null" />, for this show.
        /// </summary>
        /// <returns>The file path(s) or an empty array on cancel.</returns>
        public string[] GetMultiOpenFilePaths(string? title = null, string? filter = null,
            string? initialDirectory = null,
            bool checkFileExists = false, bool readOnlyChecked = false, bool showReadOnly = false,
            bool addExtension = true, bool validateNames = true,
            string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
            bool addToRecent = false, string? initialFileName = null)
        {
            _openFileDialog ??= new OpenFileDialog();
            var dialog = _openFileDialog;

            SetOpenFileDialogOptions(dialog, true, title, filter, initialDirectory,
                checkFileExists, readOnlyChecked, showReadOnly, addExtension, validateNames,
                defaultExt, filterIndex, dereferenceLinks, addToRecent, initialFileName);

            if (dialog.ShowDialog(Owner) != true)
                return [];

            return dialog.FileNames;
        }

        public string GetOpenFolderPath(string? title = null,
            string? initialDirectory = null, string? rootDirectory = null,
            bool validateNames = true, bool addToRecent = false, string? initialFolderName = null)
        {
            _openFolderDialog ??= new OpenFolderDialog();
            var dialog = _openFolderDialog;

            SetOpenFolderDialogOptions(dialog, title, initialDirectory, false,
                rootDirectory, validateNames, addToRecent, initialFolderName);

            if (dialog.ShowDialog(Owner) != true)
                return string.Empty;

            return dialog.FolderName;
        }

        public string[] GetMultiOpenFolderPath(string? title = null,
            string? initialDirectory = null, string? rootDirectory = null,
            bool validateNames = true, bool addToRecent = false, string? initialFolderName = null)
        {
            _openFolderDialog ??= new OpenFolderDialog();
            var dialog = _openFolderDialog;

            SetOpenFolderDialogOptions(dialog, title, initialDirectory, true,
                rootDirectory, validateNames, addToRecent, initialFolderName);

            if (dialog.ShowDialog(Owner) != true)
                return [];

            return dialog.FolderNames;
        }

        protected static void SetSaveFileDialogOptions(SaveFileDialog dialog, string? title = null,
            string? filter = null, string? initialDirectory = null,
            bool overwritePrompt = true, bool createPrompt = false, bool checkPathExists = false,
            bool addExtension = true, bool validateNames = true,
            string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
            bool restoreDirectory = false, bool addToRecent = false, string? initialFileName = null)
        {
            dialog.Title = title;
            dialog.Filter = filter ?? string.Empty;
            dialog.OverwritePrompt = overwritePrompt;
            dialog.CreatePrompt = createPrompt;
            //dialog.CheckFileExists = checkFileExists; //Ignored by SaveFileDialog
            dialog.CheckPathExists = checkPathExists;
            dialog.AddExtension = addExtension;
            dialog.ValidateNames = validateNames;
            dialog.DefaultExt = defaultExt ?? string.Empty;
            dialog.FilterIndex = filterIndex;
            dialog.DereferenceLinks = dereferenceLinks;
            dialog.RestoreDirectory = restoreDirectory;
            if (initialDirectory != null)
            {
                dialog.InitialDirectory = initialDirectory;
            }
            dialog.AddToRecent = addToRecent;

            dialog.FileName = initialFileName ?? string.Empty;
        }

        protected static void SetOpenFileDialogOptions(OpenFileDialog dialog, bool multiselect,
            string? title = null, string? filter = null, string? initialDirectory = null,
            bool checkFileExists = false, bool readOnlyChecked = false, bool showReadOnly = false,
            bool addExtension = true, bool validateNames = true,
            string? defaultExt = null, int filterIndex = 1, bool dereferenceLinks = false,
            bool addToRecent = false, string? initialFileName = null)
        {
            dialog.Multiselect = multiselect;
            dialog.Title = title;
            dialog.Filter = filter ?? string.Empty;
            dialog.CheckFileExists = checkFileExists;
            //dialog.CheckPathExists = checkPathExists; //Implied by CheckFileExists
            dialog.ReadOnlyChecked = readOnlyChecked;
            dialog.ShowReadOnly = showReadOnly;
            dialog.AddExtension = addExtension;
            dialog.ValidateNames = validateNames;
            dialog.DefaultExt = defaultExt ?? string.Empty;
            dialog.FilterIndex = filterIndex;
            dialog.DereferenceLinks = dereferenceLinks;
            //dialog.RestoreDirectory = restoreDirectory; //Not for use with OpenFileDialog
            if (initialDirectory != null)
            {
                dialog.InitialDirectory = initialDirectory;
            }
            dialog.AddToRecent = addToRecent;

            dialog.FileName = initialFileName ?? string.Empty;
        }

        protected static void SetOpenFolderDialogOptions(OpenFolderDialog dialog, string? title = null,
            string? initialDirectory = null, bool multiselect = false, string? rootDirectory = null,
            bool validateNames = true, bool addToRecent = false, string? initialFolderName = null)
        {
            dialog.Title = title;
            if (initialDirectory != null)
            {
                dialog.InitialDirectory = initialDirectory;
            }
            dialog.Multiselect = multiselect;
            dialog.RootDirectory = rootDirectory;
            dialog.ValidateNames = validateNames;
            dialog.AddToRecent = addToRecent;
            dialog.FolderName = initialFolderName;
        }
    }
}