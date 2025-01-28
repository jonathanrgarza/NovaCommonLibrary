using System.Windows;

namespace Ncl.Common.Wpf.Infrastructure;

/// <summary>
/// Interface for a window factory that creates windows.
/// </summary>
public interface IWindowFactory
{
    /// <summary>
    /// Creates a new window.
    /// </summary>
    /// <returns>Creates a new instance of the window.</returns>
    Window Create();
}

/// <summary>
/// Interface for a window factory that creates windows.
/// </summary>
/// <typeparam name="T">The Window type.</typeparam>
public interface IWindowFactory<out T> : IWindowFactory
    where T : Window
{
    /// <summary>
    /// Creates a new window.
    /// </summary>
    /// <returns>Creates a new instance of the window.</returns>
    new T Create();
}