using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// Special view model for the common item dialog. It is used to open a dialog and get the selected item.
/// </summary>
public abstract class ItemCommonDialogViewModel : CommonDialogViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ItemCommonDialogViewModel"/> class.
    /// </summary>
    /// <param name="defaultDirectory">The default directory for the common item dialog.</param>
    /// <param name="initialDirectory">The initial directory for the common item dialog.</param>
    /// <param name="rootDirectory">The root directory for the common item dialog.</param>
    /// <param name="title">The title of the common item dialog.</param>
    protected ItemCommonDialogViewModel(
        string title = "",
        string initialDirectory = "",
        string defaultDirectory = "",
        string rootDirectory = "")
    {
        InitializeValues();
        DefaultDirectory = defaultDirectory;
        InitialDirectory = initialDirectory;
        RootDirectory = rootDirectory;
        Title = title;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to add the selected item to the recent items list.
    /// </summary>
    public bool AddToRecent { get; set; }

    /// <summary>
    /// Gets or sets a Guid to associate with the dialog's persisted state.
    /// </summary>
    public Guid? ClientGuid { get; set; }

    /// <summary>
    /// Gets or sets the custom places for the file dialog.
    /// </summary>
    public IList<FileDialogCustomPlace>? CustomPlaces { get; set; }

    /// <summary>
    /// Gets or sets the directory displayed by the file dialog box
    /// if there is not a recently used directory value available.
    /// </summary>
    public string DefaultDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box returns the location
    /// of the file referenced by the shortcut or whether it returns the location
    /// of the shortcut (.lnk). Not all dialogs allow users to select shortcuts.
    /// </summary>
    public bool DereferenceLinks { get; set; }

    /// <summary>
    /// Gets or sets the initial directory for the file dialog.
    /// </summary>
    public string InitialDirectory { get; set; }

    /// <summary>
    /// Gets or sets the directory displayed as the navigation root for the dialog.
    /// Items in the navigation pane are replaced with the specified item, to guide the user
    /// from navigating outside the namespace.
    /// </summary>
    public string RootDirectory { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box will show
    /// hidden and system items regardless of user preferences.
    /// </summary>
    public bool ShowHiddenItems { get; set; }

    /// <summary>
    /// Gets or sets a string shown in the title bar of the file dialog.
    /// If this property is null, a localized default from the operating
    /// system itself will be used (typically something like "Save As" or "Open")
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to check for situations that would prevent
    /// an application from opening the selected file, such as sharing violations or access denied errors.
    /// </summary>
    public bool ValidateNames { get; set; }

    /// <summary>
    /// Initializes the values to default values.
    /// </summary>
    private void InitializeValues()
    {
        DialogResult = null;
        AddToRecent = false;
        ClientGuid = null;
        CustomPlaces = null;
        DefaultDirectory = string.Empty;
        DereferenceLinks = true;
        InitialDirectory = string.Empty;
        RootDirectory = string.Empty;
        ShowHiddenItems = false;
        Tag = null;
        Title = string.Empty;
        ValidateNames = true;
    }

    /// <inheritdoc/>
    public override void Reset()
    {
        base.Reset();
        InitializeValues();
    }
}