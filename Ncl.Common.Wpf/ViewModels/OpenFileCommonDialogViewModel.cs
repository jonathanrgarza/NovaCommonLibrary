namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// Special view model for the OpenFileDialog. It is used to open the file dialog and get the selected path(s).
/// </summary>
public sealed class OpenFileCommonDialogViewModel : FileCommonDialogViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OpenFileCommonDialogViewModel"/> class.
    /// </summary>
    /// <param name="defaultDirectory">The default directory for the file dialog.</param>
    /// <param name="initialDirectory">The initial directory for the file dialog.</param>
    /// <param name="rootDirectory">The root directory for the file dialog.</param>
    /// <param name="title">The title of the file dialog.</param>
    /// <param name="filter">The file type filter. FilterName|*.ext;*.ext2</param>
    /// <param name="defaultExt">The default/fallback file extension.</param>
    /// <param name="multiselect">Is multiple files allowed to be selected.</param>
    public OpenFileCommonDialogViewModel(string title = "",
        string initialDirectory = "",
        string defaultDirectory = "",
        string rootDirectory = "",
        string filter = "",
        string defaultExt = "",
        bool multiselect = false)
        : base(title, initialDirectory, defaultDirectory, rootDirectory,
            filter, defaultExt)
    {
        InitializeValues();
        DefaultExt = defaultExt;
        Filter = filter;
        Multiselect = multiselect;
    }

    /// <summary>
    /// Gets or sets an option flag indicating whether the dialog box forces the preview pane on.
    /// </summary>
    public bool ForcePreviewPane { get; set; }

    /// <summary>
    /// Gets or sets an option indicating whether OpenFileDialog allows users to select multiple files.
    /// </summary>
    public bool Multiselect { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the read-only checkbox displayed by OpenFileDialog is selected.
    /// </summary>
    public bool ReadOnlyChecked { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether OpenFileDialog contains a read-only checkbox.
    /// </summary>
    public bool ShowReadOnly { get; set; }

    /// <summary>
    /// Initializes the values to default values.
    /// </summary>
    private void InitializeValues()
    {
        ForcePreviewPane = false;
        Multiselect = false;
        ReadOnlyChecked = false;
        ShowReadOnly = false;
    }

    /// <inheritdoc/>
    public override void Reset()
    {
        base.Reset();
        InitializeValues();
    }
}