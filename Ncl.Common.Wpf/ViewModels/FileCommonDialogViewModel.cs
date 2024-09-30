using System;
using System.IO;
using System.Linq;

namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// Special view model for the file dialog. It is used to open a dialog and get the selected item(s).
/// </summary>
public abstract class FileCommonDialogViewModel : ItemCommonDialogViewModel
{
    private string[] _fileNames = [];
    private int _filterIndex;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileCommonDialogViewModel"/> class.
    /// </summary>
    /// <param name="defaultDirectory">The default directory for the file dialog.</param>
    /// <param name="initialDirectory">The initial directory for the file dialog.</param>
    /// <param name="rootDirectory">The root directory for the file dialog.</param>
    /// <param name="title">The title of the file dialog.</param>
    /// <param name="filter">The file type filter. FilterName|*.ext;*.ext2</param>
    /// <param name="defaultExt">The default/fallback file extension.</param>
    protected FileCommonDialogViewModel(string title = "",
        string initialDirectory = "",
        string defaultDirectory = "",
        string rootDirectory = "",
        string filter = "",
        string defaultExt = "")
        : base(title, initialDirectory, defaultDirectory, rootDirectory)
    {
        InitializeValues();
        DefaultExt = defaultExt;
        Filter = filter;
    }

    /// <summary>
    /// Gets or sets a string containing the full path of the file selected in
    /// the file dialog box.
    /// If multiple files are selected, this property only returns the first one.
    /// </summary>
    public string? FileName
    {
        get => _fileNames.Length == 0 ? string.Empty : _fileNames[0];
        set
        {
            if (value == null && _fileNames.Length == 0)
                return;
            if (value != null && _fileNames.Length == 1 && _fileNames[0] == value)
                return;

            _fileNames = value == null ? [] : [value];
            OnPropertyChanged(nameof(FileNames));
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets the file names of all selected files in the dialog box.
    /// </summary>
    public string[] FileNames
    {
        get => (string[])_fileNames.Clone();
        set
        {
            if (value.Length == 0 && _fileNames.Length == 0)
                return;
            if (_fileNames.SequenceEqual(value))
                return;

            _fileNames = (string[])value.Clone();
            OnPropertyChanged();
            OnPropertyChanged(nameof(FileName));
        }
    }

    /// <summary>
    /// Gets a string containing the filename component of the
    /// file selected in the dialog box.
    /// Example:  if FileName = "c:\windows\explorer.exe" ,
    /// SafeFileName = "explorer.exe"
    /// </summary>
    public string SafeFileName => Path.GetFileName(FileName!);

    /// <summary>
    /// Gets a string array containing the filename of each file selected
    /// in the dialog box.
    /// </summary>
    public string[] SafeFileNames
    {
        get
        {
            // Retrieve the existing filenames into an array, then make
            // another array of the same length to hold the safe version.
            string[] unsafeFileNames = FileNames;
            string[] safeFileNames = new string[unsafeFileNames.Length];

            for (int i = 0; i < unsafeFileNames.Length; i++) safeFileNames[i] = Path.GetFileName(unsafeFileNames[i]);

            return safeFileNames;
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the
    /// dialog box automatically adds an extension to a
    /// file name if the user omits the extension.
    /// </summary>
    public bool AddExtension { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether
    /// the dialog box displays a warning if the
    /// user specifies a file name that does not exist.
    /// </summary>
    public bool CheckFileExists { get; set; }

    /// <summary>
    /// Specifies that the user can type only valid paths and file names. If this flag is
    /// used and the user types an invalid path and file name in the File Name entry field,
    /// a warning is displayed in a message box.
    /// </summary>
    public bool CheckPathExists { get; set; }

    /// <summary>
    /// The AddExtension property attempts to determine the appropriate extension
    /// by using the selected filter. The DefaultExt property serves as a fallback -
    /// if the extension cannot be determined from the filter, DefaultExt will
    /// be used instead.
    /// </summary>
    public string DefaultExt { get; set; }

    /// <summary>
    /// Gets or sets the current file name filter string,
    /// which determines the choices that appear in the "Save as file type" or
    /// "Files of type" box at the bottom of the dialog box.
    /// This is an example filter string:
    /// Filter = "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*"
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown in the setter if the new filter string does not have an even number of tokens
    /// separated by the vertical bar character '|' (that is, the new filter string is invalid.)
    /// </exception>
    public string Filter { get; set; }

    /// <summary>
    /// Gets or sets the index of the filter currently selected in the file dialog box.
    /// NOTE: The index of the first filter entry is 1, not 0.
    /// </summary>
    public int FilterIndex
    {
        get => _filterIndex;
        set
        {
            if (_filterIndex == value)
                return;
            _filterIndex = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Restores the current directory to its original value if the user
    /// changed the directory while searching for files.
    /// This property is only valid for SaveFileDialog; it has no effect
    /// when set on an OpenFileDialog.
    /// </summary>
    public bool RestoreDirectory { get; set; }

    /// <summary>
    /// Initializes the values to default values.
    /// </summary>
    private void InitializeValues()
    {
        AddExtension = true;
        CheckFileExists = false;
        CheckPathExists = false;
        DefaultExt = string.Empty;
        FileName = null;
        Filter = string.Empty;
        FilterIndex = 1;
        RestoreDirectory = false;
    }

    /// <inheritdoc/>
    public override void Reset()
    {
        base.Reset();
        InitializeValues();
    }
}