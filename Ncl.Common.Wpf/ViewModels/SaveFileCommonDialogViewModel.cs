namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// Special view model for the SaveFileDialog. It is used to open the file dialog and get the selected path.
/// </summary>
public sealed class SaveFileCommonDialogViewModel : FileCommonDialogViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SaveFileCommonDialogViewModel"/> class.
    /// </summary>
    /// <param name="defaultDirectory">The default directory for the file dialog.</param>
    /// <param name="initialDirectory">The initial directory for the file dialog.</param>
    /// <param name="rootDirectory">The root directory for the file dialog.</param>
    /// <param name="title">The title of the file dialog.</param>
    /// <param name="filter">The file type filter. FilterName|*.ext;*.ext2</param>
    /// <param name="defaultExt">The default/fallback file extension.</param>
    public SaveFileCommonDialogViewModel(string title = "",
        string initialDirectory = "",
        string defaultDirectory = "",
        string rootDirectory = "",
        string filter = "",
        string defaultExt = "")
        : base(title, initialDirectory, defaultDirectory, rootDirectory,
            filter, defaultExt)
    {
        InitializeValues();
        DefaultExt = defaultExt;
        Filter = filter;
    }

    /// <summary>
    /// Gets or sets a value indicating whether SaveFileDialog prompts the user for
    /// permission to create a file if the user specifies a file that does not exist.
    /// </summary>
    public bool CreatePrompt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the dialog box will attempt to create a test file at the selected path.
    /// </summary>
    public bool CreateTestFile { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether SaveFileDialog displays a warning if the user
    /// specifies the name of a file that already exists.
    /// </summary>
    public bool OverwritePrompt { get; set; }

    /// <summary>
    /// Initializes the values to default values.
    /// </summary>
    private void InitializeValues()
    {
        CreatePrompt = false;
        CreateTestFile = false;
        OverwritePrompt = true;
    }

    /// <inheritdoc/>
    public override void Reset()
    {
        base.Reset();
        InitializeValues();
    }
}