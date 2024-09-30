using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Ncl.Common.Wpf.ViewModels;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Service for displaying dialogs.
/// </summary>
public class DialogService : IDialogService
{
    private readonly Dictionary<Type, IWindowFactory> _mappings = new();
    private readonly IWindowManager _windowManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="DialogService"/> class.
    /// </summary>
    /// <param name="windowManager">The window manager.</param>
    public DialogService(IWindowManager windowManager)
    {
        _windowManager = windowManager;
        HandleSpecialDialogs = true;
    }

    /// <summary>
    /// Gets or sets a value indicating whether special dialogs
    /// (SaveFileDialog, OpenFileDialog, and OpenFolderDialog) should be handled by the dialog service.
    /// Default is true
    /// </summary>
    public bool HandleSpecialDialogs { get; set; }

    /// <inheritdoc/>
    public bool? ShowDialog<TViewModel>(TViewModel dataContext) where TViewModel : class
    {
        if (HandleSpecialDialogs && ShowSpecialDialog(dataContext, out bool? showDialog))
            return showDialog;

        if (_mappings.TryGetValue(typeof(TViewModel), out var dialogFactory))
        {
            if (dialogFactory == null)
                throw new InvalidOperationException(
                    $"No dialog type registered for view model of type {typeof(TViewModel)}.");

            var dialog = dialogFactory.Create();

            dialog.DataContext = dataContext;

            bool? result = _windowManager.ShowDialog(dialog);
            if (dataContext is IDialogViewModel dialogViewModel) dialogViewModel.DialogResult = result;
            return result;
        }

        throw new InvalidOperationException($"No dialog type registered for view model of type {typeof(TViewModel)}.");
    }

    /// <inheritdoc/>
    public bool IsRegistered<TViewModel>() where TViewModel : class
    {
        return _mappings.ContainsKey(typeof(TViewModel));
    }

    /// <summary>
    /// Shows a special dialog (SaveFileDialog, OpenFileDialog, OpenFolderDialog) based on the view model type.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="dataContext">The data context of the view model.</param>
    /// <param name="showDialog">The result of the dialog.</param>
    /// <returns>True if a special dialog was shown, otherwise false.</returns>
    protected bool ShowSpecialDialog<TViewModel>(TViewModel dataContext, out bool? showDialog)
        where TViewModel : class
    {
        switch (dataContext)
        {
            case SaveFileCommonDialogViewModel saveFileDialogViewModel:
            {
                var dialog = new SaveFileDialog
                {
                    AddExtension = saveFileDialogViewModel.AddExtension,
                    AddToRecent = saveFileDialogViewModel.AddToRecent,
                    CheckFileExists = saveFileDialogViewModel.CheckFileExists,
                    CheckPathExists = saveFileDialogViewModel.CheckPathExists,
                    ClientGuid = saveFileDialogViewModel.ClientGuid,
                    CreatePrompt = saveFileDialogViewModel.CreatePrompt,
                    CreateTestFile = saveFileDialogViewModel.CreateTestFile,
                    CustomPlaces = saveFileDialogViewModel.CustomPlaces,
                    DefaultExt = saveFileDialogViewModel.DefaultExt,
                    DefaultDirectory = saveFileDialogViewModel.DefaultDirectory,
                    DereferenceLinks = saveFileDialogViewModel.DereferenceLinks,
                    FileName = saveFileDialogViewModel.FileName ?? string.Empty,
                    Filter = saveFileDialogViewModel.Filter,
                    FilterIndex = saveFileDialogViewModel.FilterIndex,
                    InitialDirectory = saveFileDialogViewModel.InitialDirectory,
                    OverwritePrompt = saveFileDialogViewModel.OverwritePrompt,
                    RestoreDirectory = saveFileDialogViewModel.RestoreDirectory,
                    RootDirectory = saveFileDialogViewModel.RootDirectory,
                    ShowHiddenItems = saveFileDialogViewModel.ShowHiddenItems,
                    Title = saveFileDialogViewModel.Title,
                    Tag = saveFileDialogViewModel.Tag,
                    ValidateNames = saveFileDialogViewModel.ValidateNames
                };

                bool? result = _windowManager.ShowDialog(dialog);

                // Update the view model with the results
                saveFileDialogViewModel.FileNames = dialog.FileNames;
                saveFileDialogViewModel.FilterIndex = dialog.FilterIndex;

                saveFileDialogViewModel.DialogResult = result;
                showDialog = result;
                return true;
            }
            case OpenFileCommonDialogViewModel openFileDialogViewModel:
            {
                var dialog = new OpenFileDialog
                {
                    AddExtension = openFileDialogViewModel.AddExtension,
                    AddToRecent = openFileDialogViewModel.AddToRecent,
                    CheckFileExists = openFileDialogViewModel.CheckFileExists,
                    CheckPathExists = openFileDialogViewModel.CheckPathExists,
                    ClientGuid = openFileDialogViewModel.ClientGuid,
                    ForcePreviewPane = openFileDialogViewModel.ForcePreviewPane,
                    ReadOnlyChecked = openFileDialogViewModel.ReadOnlyChecked,
                    ShowReadOnly = openFileDialogViewModel.ShowReadOnly,
                    CustomPlaces = openFileDialogViewModel.CustomPlaces,
                    DefaultExt = openFileDialogViewModel.DefaultExt,
                    DefaultDirectory = openFileDialogViewModel.DefaultDirectory,
                    DereferenceLinks = openFileDialogViewModel.DereferenceLinks,
                    FileName = openFileDialogViewModel.FileName ?? string.Empty,
                    Filter = openFileDialogViewModel.Filter,
                    FilterIndex = openFileDialogViewModel.FilterIndex,
                    InitialDirectory = openFileDialogViewModel.InitialDirectory,
                    Multiselect = openFileDialogViewModel.Multiselect,
                    RestoreDirectory = openFileDialogViewModel.RestoreDirectory,
                    RootDirectory = openFileDialogViewModel.RootDirectory,
                    ShowHiddenItems = openFileDialogViewModel.ShowHiddenItems,
                    Title = openFileDialogViewModel.Title,
                    Tag = openFileDialogViewModel.Tag,
                    ValidateNames = openFileDialogViewModel.ValidateNames
                };

                bool? result = _windowManager.ShowDialog(dialog);

                // Update the view model with the results
                openFileDialogViewModel.FileNames = dialog.FileNames;
                openFileDialogViewModel.FilterIndex = dialog.FilterIndex;

                openFileDialogViewModel.DialogResult = result;
                showDialog = result;
                return true;
            }
            case OpenFolderCommonDialogViewModel openFolderDialogViewModel:
            {
                var dialog = new OpenFolderDialog
                {
                    AddToRecent = openFolderDialogViewModel.AddToRecent,
                    ClientGuid = openFolderDialogViewModel.ClientGuid,
                    CustomPlaces = openFolderDialogViewModel.CustomPlaces,
                    DefaultDirectory = openFolderDialogViewModel.DefaultDirectory,
                    DereferenceLinks = openFolderDialogViewModel.DereferenceLinks,
                    InitialDirectory = openFolderDialogViewModel.InitialDirectory,
                    Multiselect = openFolderDialogViewModel.Multiselect,
                    FolderName = openFolderDialogViewModel.FolderName ?? string.Empty,
                    RootDirectory = openFolderDialogViewModel.RootDirectory,
                    ShowHiddenItems = openFolderDialogViewModel.ShowHiddenItems,
                    Title = openFolderDialogViewModel.Title,
                    Tag = openFolderDialogViewModel.Tag,
                    ValidateNames = openFolderDialogViewModel.ValidateNames
                };

                bool? result = _windowManager.ShowDialog(dialog);

                // Update the view model with the results
                openFolderDialogViewModel.FolderNames = dialog.FolderNames;

                openFolderDialogViewModel.DialogResult = result;
                showDialog = result;
                return true;
            }
        }

        showDialog = null;
        return false;
    }

    /// <summary>
    /// Registers a mapping between a view model type and a dialog factory.
    /// </summary>
    /// <param name="factory">The factory to generate the dialog.</param>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <returns>Returns itself to support chaining calls.</returns>
    public DialogService Register<TViewModel>(IWindowFactory factory)
    {
        _mappings[typeof(TViewModel)] = factory;
        return this;
    }

    /// <summary>
    /// Unregisters a view model type from the dialog service.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <returns>True if the mapping was found and removed, otherwise, false.</returns>
    public bool Unregister<TViewModel>()
    {
        return _mappings.Remove(typeof(TViewModel));
    }
}