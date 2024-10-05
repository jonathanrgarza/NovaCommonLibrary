using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
using Ncl.Common.Core.Collections;
using Ncl.Common.Core.Extensions;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Manages the open windows and dialogs in the application.
/// </summary>
public class WindowManager : IWindowManager
{
    private readonly Stack<Window> _openDialogs = [];
    private readonly HashSet<Window> _openWindows = [];
    private readonly Dictionary<Type, IWindowFactory> _windowMappings = new();

    private Window? _activeWindow;
    private Window? _mainWindow;
    private IReadOnlyStack<Window>? _readonlyOpenDialogs;
    private IReadOnlyCollectionWrapper<Window>? _readonlyOpenWindows;

    /// <inheritdoc/>
    public IReadOnlyCollectionWrapper<Window> OpenWindows => _readonlyOpenWindows ??= _openWindows.AsReadOnly();

    /// <inheritdoc/>
    public IReadOnlyStack<Window> OpenDialogs => _readonlyOpenDialogs ??= _openDialogs.AsReadOnly();

    /// <inheritdoc/>
    public Window? ActiveWindow
    {
        get => _activeWindow;
        private set
        {
            if (_activeWindow == value) return;

            Debug.WriteLine($"Active Window Changed: {_activeWindow} -> {value}");

            _activeWindow = value;
            OnActiveWindowChanged();
        }
    }

    /// <inheritdoc/>
    public Window? MainWindow
    {
        get => _mainWindow;
        private set
        {
            if (_mainWindow == value) return;

            //Debug.WriteLine($"Main Window Changed: {_mainWindow} -> {value}");

            _mainWindow = value;
            OnMainWindowChanged();
        }
    }

    /// <inheritdoc/>
    public void ShowWindowFromViewModel<TViewModel, T>(TViewModel dataContext, bool assignParent = false)
        where TViewModel : class
    {
        if (!_windowMappings.TryGetValue(typeof(TViewModel), out var windowFactory))
        {
            throw new InvalidOperationException($"No window registered for {typeof(TViewModel).Name}");
        }

        var window = windowFactory.Create();
        window.DataContext = dataContext;
        ShowWindow(window, assignParent);
    }

    /// <inheritdoc/>
    public void ShowWindowFromViewModel<TViewModel, T>(bool assignParent = false)
        where TViewModel : class
    {
        if (!_windowMappings.TryGetValue(typeof(TViewModel), out var windowFactory))
        {
            throw new InvalidOperationException($"No window registered for {typeof(TViewModel).Name}");
        }

        var window = windowFactory.Create();
        if (window.DataContext == null)
        {
            throw new InvalidOperationException($"Window DataContext is null for {typeof(TViewModel).Name}");
        }

        ShowWindow(window, assignParent);
    }

    /// <inheritdoc/>
    public void ShowWindow(Window window, bool assignParent = false)
    {
        if (assignParent && ActiveWindow != null)
        {
            window.Owner = ActiveWindow;
        }

        _openWindows.Add(window);

        window.Closed += OnWindowClosed;

        window.Show();

        MainWindow ??= window;

        if (window.IsActive)
        {
            ActiveWindow = window;
        }

        window.Activated += OnWindowActivated;
        window.Deactivated += OnWindowDeactivated;
    }

    /// <inheritdoc/>
    public bool? ShowDialog(Window window, bool assignParent = true)
    {
        if (assignParent && ActiveWindow != null)
        {
            window.Owner = ActiveWindow;
        }

        _openWindows.Add(window);
        _openDialogs.Push(window);

        ActiveWindow = window;
        MainWindow ??= window;

        bool? result = window.ShowDialog();
        // The OnWindowActivated will already have fired for the parent window, no need to remove ActiveWindow
        _openWindows.Remove(window);
        _openDialogs.Pop();

        return result;
    }

    /// <inheritdoc/>
    public bool? ShowDialog(CommonDialog dialog, bool assignParent = true)
    {
        var parent = assignParent ? ActiveWindow : null;
        bool? result = dialog.ShowDialog(parent);
        return result;
    }

    /// <inheritdoc/>
    public event EventHandler<Window?>? ActiveWindowChanged;

    /// <inheritdoc/>
    public event EventHandler<Window?>? MainWindowChanged;

    /// <summary>
    /// Registers a window for a view model.
    /// </summary>
    /// <typeparam name="TViewModel">The view model type.</typeparam>
    /// <param name="windowFactory">The window factory.</param>
    public void RegisterWindow<TViewModel>(IWindowFactory windowFactory)
    {
        _windowMappings[typeof(TViewModel)] = windowFactory;
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        if (sender is not Window window) return;

        _openWindows.Remove(window);
        if (ActiveWindow == window)
        {
            ActiveWindow = null;
        }

        // Unsubscribe from events
        window.Closed -= OnWindowClosed;
        window.Activated -= OnWindowActivated;
        window.Deactivated -= OnWindowDeactivated;
    }

    private void OnWindowActivated(object? sender, EventArgs e)
    {
        if (sender is not Window window) return;
        ActiveWindow = window;
    }

    private void OnWindowDeactivated(object? sender, EventArgs e)
    {
        if (sender is not Window window) return;

        if (ActiveWindow == window)
        {
            ActiveWindow = null;
        }
    }

    private void OnActiveWindowChanged()
    {
        // Do something when the active window changes
        ActiveWindowChanged?.Invoke(this, ActiveWindow);
    }

    private void OnMainWindowChanged()
    {
        MainWindowChanged?.Invoke(this, MainWindow);
    }
}