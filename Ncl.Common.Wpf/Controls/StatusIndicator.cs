using System;
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
/// A control that displays a status indicator.
/// </summary>
public class StatusIndicator : Control
{
    /// <summary>
    /// Identifies the <see cref="Status"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty StatusProperty = DependencyProperty.Register(
        nameof(Status),
        typeof(IndicatorStatus),
        typeof(StatusIndicator),
        new PropertyMetadata(IndicatorStatus.None, OnStatusChanged));

    /// <summary>
    /// Identifies the <see cref="UseAnimations"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty UseAnimationsProperty = DependencyProperty.Register(
        nameof(UseAnimations), typeof(bool), typeof(StatusIndicator), new PropertyMetadata(true));

    /// <summary>
    /// Is the control using animations.
    /// </summary>
    public bool UseAnimations
    {
        get => (bool)GetValue(UseAnimationsProperty);
        set => SetValue(UseAnimationsProperty, value);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="StatusIndicator"/> class.
    /// </summary>
    static StatusIndicator()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(StatusIndicator),
            new FrameworkPropertyMetadata(typeof(StatusIndicator)));
    }

    /// <summary>
    /// Gets or sets the status of the indicator.
    /// </summary>
    public IndicatorStatus Status
    {
        get => (IndicatorStatus)GetValue(StatusProperty);
        set => SetValue(StatusProperty, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        UpdateVisualState(false);
    }

    /// <summary>
    /// Called when the status changes.
    /// </summary>
    /// <param name="d">The dependency object that changed.</param>
    /// <param name="e">The event arguments.</param>
    private static void OnStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((StatusIndicator)d).UpdateVisualState(true);
    }

    /// <summary>
    /// Updates the visual state of the control.
    /// </summary>
    /// <param name="useTransitions">Should transitions be used, if allowed by <see cref="UseAnimations"/>.</param>
    private void UpdateVisualState(bool useTransitions)
    {
        bool useAnimations = UseAnimations && useTransitions;
        switch (Status)
        {
            case IndicatorStatus.None:
                VisualStateManager.GoToState(this, "None", useAnimations);
                break;
            case IndicatorStatus.Success:
                VisualStateManager.GoToState(this, "Success", useAnimations);
                break;
            case IndicatorStatus.Info:
                VisualStateManager.GoToState(this, "Info", useAnimations);
                break;
            case IndicatorStatus.Warning:
                VisualStateManager.GoToState(this, "Warning", useAnimations);
                break;
            case IndicatorStatus.Error:
                VisualStateManager.GoToState(this, "Error", useAnimations);
                break;
            case IndicatorStatus.Faulted:
                VisualStateManager.GoToState(this, "Faulted", useAnimations);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}