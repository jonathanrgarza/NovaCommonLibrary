using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
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
        /// <remarks>Works on an unminified window regardless if the owning process is elevated.</remarks>
        /// <param name="windowTitle">The window's title.</param>
        /// <returns>true if the window was found and set as foreground window, otherwise, false.</returns>
        public static bool SetForegroundWindow(string windowTitle)
        {
            var targetWindowHandle = FindWindowByTitle(windowTitle);

            return targetWindowHandle != IntPtr.Zero && NativeMethods.SetForegroundWindow(targetWindowHandle);
        }

        /// <summary>
        /// Sets the foreground window by its handle.
        /// </summary>
        /// <remarks>Works on an unminified window regardless if the owning process is elevated.</remarks>
        /// <param name="windowHandle">The window's handle.</param>
        /// <returns>true if the window was found and set as foreground window, otherwise, false.</returns>
        public static bool SetForegroundWindow(IntPtr windowHandle)
        {
            return NativeMethods.SetForegroundWindow(windowHandle);
        }

        /// <summary>
        /// Brings a window to the foreground without focusing on it.
        /// </summary>
        /// <remarks>Does not work on another process's window if the process is elevated and the calling process is not.</remarks>
        /// <param name="windowHandle">The window's handle.</param>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">A win32 exception occurs.</exception>
        public static void BringWindowToForeground(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(windowHandle));

            // Check if the window is minimized, if so, restore it
            int errorCode;
            if (NativeMethods.IsIconic(windowHandle))
            {
                if (!NativeMethods.ShowWindowAsync(windowHandle, NativeMethods.SW_RESTORE))
                {
                    errorCode = Marshal.GetLastWin32Error();
                    throw new Win32Exception(errorCode);
                }
            }

            bool success = NativeMethods.SetWindowPos(windowHandle,
                NativeMethods.HWND_TOP,
                0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE | NativeMethods.SWP_SHOWWINDOW);

            if (success) 
                return;
        
            errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode);
        
        }

        /// <summary>
        /// Sends a window to the back without activating it.
        /// </summary>
        /// <remarks>Does not work on another process's window if the process is elevated and the calling process is not.</remarks>
        /// <param name="windowHandle">The window's handle.</param>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        /// <exception cref="System.ComponentModel.Win32Exception">A win32 exception occurs.</exception>
        public static void SendWindowToBackground(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(windowHandle));

            bool success = NativeMethods.SetWindowPos(windowHandle,
                NativeMethods.HWND_BOTTOM,
                0, 0, 0, 0,
                NativeMethods.SWP_NOMOVE | NativeMethods.SWP_NOSIZE | NativeMethods.SWP_NOACTIVATE);

            if (success) 
                return;
            int errorCode = Marshal.GetLastWin32Error();
            throw new Win32Exception(errorCode);
        }

        /// <summary>
        /// Checks if a window is minimized by its handle.
        /// </summary>
        /// <param name="windowHandle">The window's handle.</param>
        /// <returns>true if the window is minimized, otherwise, false.</returns>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        public static bool IsWindowMinimized(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(windowHandle));

            return NativeMethods.IsIconic(windowHandle);
        }

        /// <summary>
        /// Checks if a window is maximized by its handle.
        /// </summary>
        /// <param name="windowHandle">The window's handle.</param>
        /// <returns>true if the window is maximized, otherwise, false.</returns>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        public static bool IsWindowMaximized(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(windowHandle));

            return NativeMethods.IsZoomed(windowHandle);
        }

        /// <summary>
        /// Restores a window, from being minimized, by its handle.
        /// </summary>
        /// <remarks>Does not work on another process's window if the process is elevated and the calling process is not.</remarks>
        /// <param name="windowHandle">The window's handle.</param>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        public static void RestoreWindow(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(windowHandle));

            // Check if the window is minimized, if so, restore it
            if (!NativeMethods.IsIconic(windowHandle)) 
                return;
            NativeMethods.ShowWindowAsync(windowHandle, NativeMethods.SW_RESTORE);
        }

        /// <summary>
        /// Minimizes a window by its handle.
        /// </summary>
        /// <remarks>Does not work on another process's window if the process is elevated and the calling process is not.</remarks>
        /// <param name="windowHandle">The window's handle.</param>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        public static void MinimizeWindow(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(windowHandle));

            // Check if the window is minimized, if so, restore it
            if (NativeMethods.IsIconic(windowHandle)) 
                return;
            NativeMethods.ShowWindowAsync(windowHandle, NativeMethods.SW_MINIMIZE);
        }

        /// <summary>
        /// Maximizes a window by its handle.
        /// </summary>
        /// <remarks>Does not work on another process's window if the process is elevated and the calling process is not.</remarks>
        /// <param name="windowHandle">The window's handle.</param>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        public static void MaximizeWindow(IntPtr windowHandle)
        {
            if (windowHandle == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(windowHandle));

            NativeMethods.ShowWindowAsync(windowHandle, NativeMethods.SW_SHOWMAXIMIZED);
        }

        /// <summary>
        /// Checks if a window's process is elevated.
        /// </summary>
        /// <remarks>Does not work on another process's window if the process is elevated and the calling process is not.</remarks>
        /// <param name="hWnd">The window's handle.</param>
        /// <returns>True if a window's handle is elevated, otherwise, false.</returns>
        /// <exception cref="ArgumentException">The handle is invalid (zero).</exception>
        /// <exception cref="Win32Exception">If a win32 exception occurs or the process status could not be retrieved.</exception>
        public static bool IsWindowProcessElevated(IntPtr hWnd)
        {
            if (hWnd == IntPtr.Zero)
                throw new ArgumentException("The window handle is invalid.", nameof(hWnd));
            uint threadHandle = NativeMethods.GetWindowThreadProcessId(hWnd, out uint processId);
            if (threadHandle == 0)
            {
                int errorCode = Marshal.GetLastWin32Error();
                throw new Win32Exception(errorCode);
            }

            var process = Process.GetProcessById((int)processId);
            return IsProcessElevated(process);
        }

        /// <summary>
        /// Checks if a window's process is elevated.
        /// </summary>
        /// <remarks>Does not work on another process if the process is elevated and the calling process is not.</remarks>
        /// <param name="process">The process.</param>
        /// <returns>True if the process is elevated, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">process is null.</exception>
        /// <exception cref="Win32Exception">If a win32 exception occurs or the process status could not be retrieved.</exception>
        public static bool IsProcessElevated(Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            NativeMethods.OpenProcessToken(process.Handle, NativeMethods.TOKEN_QUERY, out var tokenHandle);
            using (var token = new NativeHandle(tokenHandle, handle => NativeMethods.CloseHandle(handle)))
            {
                uint tokenInfoLength = (uint)Marshal.SizeOf(typeof(NativeMethods.TOKEN_ELEVATION));
                bool result = NativeMethods.GetTokenInformation(token.Handle,
                    NativeMethods.TOKEN_INFORMATION_CLASS.TokenElevation,
                    out var elevationResult, tokenInfoLength, out _);
                if (result)
                {
                    return elevationResult.TokenIsElevated != 0;
                }

                throw new Win32Exception("Failed to get process token information.");
            }
        }
    }
}
