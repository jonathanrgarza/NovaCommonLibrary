using Ncl.Common.Core.UI;

namespace Ncl.Common.Wpf.ViewModels;

/// <summary>
/// View model for dialogs. It is used to open a dialog and get the dialog result.
/// </summary>
public abstract class CommonDialogViewModel : ViewModelBase, IDialogViewModel
{
    private bool? _dialogResult;
    private object? _tag;

    /// <summary>
    /// Gets or sets the dialog result.
    /// </summary>
    public bool? DialogResult
    {
        get => _dialogResult;
        set
        {
            if (_dialogResult == value) 
                return;
            _dialogResult = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Gets or sets an arbitrary object to the dialog.
    /// </summary>
    public object? Tag
    {
        get => _tag;
        set
        {
            if (Equals(_tag, value)) 
                return;
            _tag = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Initializes the values to default values.
    /// </summary>
    private void InitializeValues()
    {
        DialogResult = null;
        Tag = null;
    }

    /// <summary>
    /// Resets value to initial values.
    /// </summary>
    public virtual void Reset()
    {
        InitializeValues();
    }
}