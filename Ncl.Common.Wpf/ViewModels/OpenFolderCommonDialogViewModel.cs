using System.IO;
using System.Linq;

namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// Special view model for the open folder dialog. It is used to open a dialog and get the selected folder.
/// </summary>
public sealed class OpenFolderCommonDialogViewModel : ItemCommonDialogViewModel
{
    private string[] _folderNames = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="OpenFolderCommonDialogViewModel"/> class.
    /// </summary>
    /// <param name="title">The title of the open folder dialog.</param>
    /// <param name="initialDirectory">The initial directory for the open folder dialog.</param>
    /// <param name="defaultDirectory">The default directory for the open folder dialog.</param>
    /// <param name="rootDirectory">The root directory for the open folder dialog.</param>
    /// <param name="multiselect">Is multiple folders allowed to be selected.</param>
    public OpenFolderCommonDialogViewModel(string title = "",
        string initialDirectory = "",
        string defaultDirectory = "",
        string rootDirectory = "",
        bool multiselect = false)
        : base(title, initialDirectory, defaultDirectory, rootDirectory)
    {
        InitializeValues();
        Multiselect = multiselect;
    }

    /// <summary>
    /// Gets or sets the full path of the folder selected in the folder dialog box.
    /// If multiple folders are selected, this property only returns the first one.
    /// </summary>
    public string? FolderName
    {
        get => _folderNames.Length == 0 ? string.Empty : _folderNames[0];
        set
        {
            if (value == null && _folderNames.Length == 0)
                return;
            if (value != null && _folderNames.Length == 1 && _folderNames[0] == value)
                return;

            _folderNames = value == null ? [] : [value];
            OnPropertyChanged(nameof(FolderNames));
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the folder names of all selected folders in the dialog box.
    /// </summary>
    public string[] FolderNames
    {
        get => (string[])_folderNames.Clone();
        set
        {
            if (value.Length == 0 && _folderNames.Length == 0)
                return;
            if (_folderNames.SequenceEqual(value))
                return;

            _folderNames = (string[])value.Clone();
            OnPropertyChanged();
            OnPropertyChanged(nameof(FolderName));
        }
    }

    /// <summary>
    /// Gets the folder name component of the folder selected in the dialog box.
    /// Example:  if FolderName = "c:\windows" ,
    /// SafeFolderName = "windows"
    /// </summary>
    public string SafeFolderName => Path.GetFileName(FolderName!);

    /// <summary>
    /// Gets the names of all folders selected in the dialog box.
    /// </summary>
    public string[] SafeFolderNames
    {
        get
        {
            // Retrieve the existing filenames into an array, then make
            // another array of the same length to hold the safe version.
            string[] unsafeFileNames = FolderNames;
            string[] safeFileNames = new string[unsafeFileNames.Length];

            for (int i = 0; i < unsafeFileNames.Length; i++) safeFileNames[i] = Path.GetFileName(unsafeFileNames[i]);

            return safeFileNames;
        }
    }

    /// <summary>
    /// Gets or sets an option flag indicating whether the dialog box allows multiple folders to be selected.
    /// </summary>
    public bool Multiselect { get; set; }

    /// <summary>
    /// Initializes the values to default values.
    /// </summary>
    private void InitializeValues()
    {
        Multiselect = false;
    }

    /// <inheritdoc/>
    public override void Reset()
    {
        base.Reset();
        InitializeValues();
    }
}