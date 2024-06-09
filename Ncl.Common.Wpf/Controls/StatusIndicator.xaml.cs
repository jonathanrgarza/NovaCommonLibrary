using System.Windows;
using System.Windows.Controls;

namespace Ncl.Common.Wpf.Controls;

/// <summary>
/// The status of the indicator.
/// </summary>
public enum IndicatorStatus
{
    /// <summary>
    /// No (off) status.
    /// </summary>
    None,

    /// <summary>
    /// Success status.
    /// </summary>
    Success,

    /// <summary>
    /// Info status.
    /// </summary>
    Info,

    /// <summary>
    /// Warning status.
    /// </summary>
    Warning,

    /// <summary>
    /// Error status.
    /// </summary>
    Error,

    /// <summary>
    /// Faulted error status.
    /// </summary>
    Faulted
}

/// <summary>
/// Interaction logic for StatusIndicator.xaml
/// </summary>
public partial class StatusIndicator : UserControl
{
    /// <summary>
    /// The status property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
        nameof(Status),
        typeof(IndicatorStatus),
        typeof(StatusIndicator),
        new PropertyMetadata(IndicatorStatus.None));

    /// <summary>
    /// Creates a new instance of the <see cref="StatusIndicator"/> class.
    /// </summary>
    public StatusIndicator()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Gets or sets the status of the indicator.
    /// </summary>
    public IndicatorStatus Status
    {
        get => (IndicatorStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }
}