using System;
using System.Windows;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Represents a factory for creating instances of a specific type of window.
/// </summary>
/// <typeparam name="T">The type of window to create.</typeparam>
public class WindowFactory<T>(Func<T> createMethod) : IWindowFactory<T>
    where T : Window
{
    private readonly Func<T> _createMethod = createMethod ?? throw new ArgumentNullException(nameof(createMethod));

    /// <inheritdoc/>
    Window IWindowFactory.Create()
    {
        return _createMethod();
    }

    /// <inheritdoc/>
    public T Create()
    {
        return _createMethod();
    }
}