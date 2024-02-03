using System;
using System.Text;
using Ncl.Common.Windows.Native;

namespace Ncl.Common.Windows.Utilities
{
    /// <summary>
    ///     Utility for dealing with windows.
    /// </summary>
    public static class WindowUtility
    {
        /// <summary>
        ///     Find a window handle by its title.
        /// </summary>
        /// <param name="title">The title to look for.</param>
        /// <returns>The window's handle or IntPtr.<see cref="IntPtr.Zero"/></returns>
        public static IntPtr FindWindowByTitle(string title)
        {
            var handle = IntPtr.Zero;
            NativeMethods.EnumWindows((hWnd, _) =>
            {
                var windowText = new StringBuilder(256);
                NativeMethods.GetWindowText(hWnd, windowText, windowText.Capacity);
                if (windowText.ToString() != title)
                    return true;

                handle = hWnd;
                return false;
            }, IntPtr.Zero);
            return handle;
        }

        /// <summary>
        /// Sets the foreground window by its title.
        /// </summary>
        /// <param name="windowTitle">The window's title.</param>
        /// <returns>true if the window was found and set as foreground window, otherwise, false.</returns>
        public static bool SetForegroundWindow(string windowTitle)
        {
            var targetWindowHandle = FindWindowByTitle(windowTitle);

            if (targetWindowHandle == IntPtr.Zero)
                return false;

            NativeMethods.SetForegroundWindow(targetWindowHandle);
            return true;
        }

        /// <summary>
        /// Sets the foreground window by its title.
        /// </summary>
        /// <param name="windowHandle">The window's handle.</param>
        /// <returns>true if the window was found and set as foreground window, otherwise, false.</returns>
        public static void SetForegroundWindow(IntPtr windowHandle)
        {
            NativeMethods.SetForegroundWindow(windowHandle);
        }
    }
}