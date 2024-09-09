using System;
using System.Collections.Generic;
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
    /// The default duration of the animation.
    /// </summary>
    public static readonly Duration DefaultAnimationDuration = new(TimeSpan.FromSeconds(0.3));

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
    /// Identifies the <see cref="AnimationDuration"/> dependency property.
    /// </summary>
    public static readonly DependencyProperty AnimationDurationProperty = DependencyProperty.Register(
        nameof(AnimationDuration),
        typeof(Duration),
        typeof(StatusIndicator),
        new PropertyMetadata(DefaultAnimationDuration, OnAnimationDurationChanged));

    private FrameworkElement? _rootElement;

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

    /// <summary>
    /// Is the control using animations.
    /// </summary>
    public bool UseAnimations
    {
        get => (bool)GetValue(UseAnimationsProperty);
        set => SetValue(UseAnimationsProperty, value);
    }

    /// <summary>
    /// Gets or sets the duration of the animation.
    /// </summary>
    public Duration AnimationDuration
    {
        get => (Duration)GetValue(AnimationDurationProperty);
        set => SetValue(AnimationDurationProperty, value);
    }

    /// <inheritdoc/>
    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _rootElement = GetTemplateChild("PART_Root") as FrameworkElement;
        UpdateVisualState(false);
        UpdateAnimationDurations();
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
    /// Called when the animation duration changes.
    /// </summary>
    /// <param name="d">The dependency object that changed.</param>
    /// <param name="e">The event arguments.</param>
    private static void OnAnimationDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((StatusIndicator)d).UpdateAnimationDurations();
    }

    /// <summary>
    /// Updates the visual state of the control.
    /// </summary>
    /// <param name="useTransitions">Should transitions be used, if allowed by <see cref="UseAnimations"/>.</param>
    /// <exception cref="ArgumentOutOfRangeException">Status is out of range.</exception>
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
                throw new ArgumentOutOfRangeException(nameof(Status));
        }
    }

    private void UpdateAnimationDurations()
    {
        if (_rootElement == null)
            return;

        var groups = VisualStateManager.GetVisualStateGroups(_rootElement);
        if (groups == null || groups.Count == 0)
            return;

        var group = groups[0] as VisualStateGroup;
        if (group?.States is not IList<VisualState?> states || states.Count == 0)
            return;

        foreach (var state in states)
        {
            var storyboard = state?.Storyboard;
            var animation = storyboard?.Children[0];
            if (animation == null)
                continue;

            animation.Duration = AnimationDuration;
        }
    }
}