using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace Ncl.Common.Windows.Native
{

    public class NativeMethods
    {
        /// <summary>
        /// Hides the window and activates another window.
        /// </summary>
        public const int SW_HIDE = 0;

        /// <summary>
        /// Activates and displays a window.
        /// If the window is minimized, maximized, or arranged, the system restores it to its original size and position.
        /// An application should specify this flag when displaying the window for the first time.
        /// </summary>
        public const int SW_SHOWNORMAL = 1;

        /// <summary>
        /// Activates the window and displays it as a minimized window.
        /// </summary>
        public const int SW_SHOWMINIMIZED = 2;

        /// <summary>
        /// Activates the window and displays it as a maximized window.
        /// </summary>
        public const int SW_SHOWMAXIMIZED = 3;

        /// <summary>
        /// Displays a window in its most recent size and position.
        /// This value is similar to SW_SHOWNORMAL, except that the window is not activated.
        /// </summary>
        public const int SW_SHOWNOACTIVATE = 4;

        /// <summary>
        /// Activates the window and displays it in its current size and position.
        /// </summary>
        public const int SW_SHOW = 5;

        /// <summary>
        /// Minimizes the specified window and activates the next top-level window in the Z order.
        /// </summary>
        public const int SW_MINIMIZE = 6;

        /// <summary>
        /// Displays the window as a minimized window.
        /// This value is similar to SW_SHOWMINIMIZED, except the window is not activated.
        /// </summary>
        public const int SW_SHOWMINNOACTIVE = 7;

        /// <summary>
        /// Displays the window in its current size and position.
        /// This value is similar to SW_SHOW, except that the window is not activated.
        /// </summary>
        public const int SW_SHOWNA = 8;

        /// <summary>
        /// Activates and displays the window.
        /// If the window is minimized, maximized, or arranged, the system restores it to its original size and position.
        /// An application should specify this flag when restoring a minimized window.
        /// </summary>
        public const int SW_RESTORE = 9;

        /// <summary>
        /// Sets the show state based on the SW_ value specified in the STARTUPINFO structure
        /// passed to the CreateProcess function by the program that started the application.
        /// </summary>
        public const int SW_SHOWDEFAULT = 10;

        /// <summary>
        /// Minimizes a window, even if the thread that owns the window is not responding.
        /// This flag should only be used when minimizing windows from a different thread.
        /// </summary>
        public const int SW_FORCEMINIMIZE = 11;

        /// <summary>
        /// Sets the specified window's show state.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nCmdShow">
        /// Controls how the window is to be shown.
        /// This parameter is ignored the first time an application calls ShowWindow,
        /// if the program that launched the application provides a STARTUPINFO structure.
        /// Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter.
        /// In subsequent calls, this parameter can be one of the following values.
        /// </param>
        /// <returns>
        /// If the window was previously visible, the return value is true.
        /// If the window was previously hidden, the return value is false.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Sets the show state of a window without waiting for the operation to complete.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="nCmdShow">
        /// Controls how the window is to be shown.
        /// This parameter is ignored the first time an application calls ShowWindow,
        /// if the program that launched the application provides a STARTUPINFO structure.
        /// Otherwise, the first time ShowWindow is called, the value should be the value obtained by the WinMain function in its nCmdShow parameter.
        /// In subsequent calls, this parameter can be one of the following values.
        /// </param>
        /// <returns>
        /// If the operation was successfully started, the return value is nonzero.
        /// </returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// Brings the thread that created the specified window into the foreground and activates the window.
        /// Keyboard input is directed to the window, and various visual cues are changed for the user.
        /// The system assigns a slightly higher priority to the thread that created the foreground window than it does to other threads.
        /// </summary>
        /// <param name="hWnd">A handle to the window that should be activated and brought to the foreground.</param>
        /// <returns>If the window was brought to the foreground, the return value is true, otherwise, false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        // Import the FindWindow function from user32.dll
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Import the EnumWindows function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        // Import the GetWindowText function from user32.dll
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// Places the window at the bottom of the Z order.
        /// If the hWnd parameter identifies a topmost window,
        /// the window loses its topmost status and is placed at the bottom of all other windows.
        /// </summary>
        public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>
        /// Places the window above all non-topmost windows (that is, behind all topmost windows).
        /// This flag has no effect if the window is already a non-topmost window.
        /// </summary>
        public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);

        /// <summary>
        /// Places the window at the top of the Z order.
        /// </summary>
        public static readonly IntPtr HWND_TOP = new IntPtr(0);

        /// <summary>
        /// Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
        /// </summary>
        public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);

        /// <summary>
        /// Retains the current size (ignores the cx and cy parameters).
        /// </summary>
        public const UInt32 SWP_NOSIZE = 0x0001;

        /// <summary>
        /// Retains the current position (ignores X and Y parameters).
        /// </summary>
        public const UInt32 SWP_NOMOVE = 0x0002;

        /// <summary>
        /// Does not activate the window.
        /// If this flag is not set, the window is activated and
        /// moved to the top of either the topmost or non-topmost group
        /// (depending on the setting of the hWndInsertAfter parameter).
        /// </summary>
        public const UInt32 SWP_NOACTIVATE = 0x0010;

        /// <summary>
        /// Displays the window.
        /// </summary>
        public const UInt32 SWP_SHOWWINDOW = 0x0040;

        /// <summary>
        /// Hides the window.
        /// </summary>
        public const UInt32 SWP_HIDEWINDOW = 0x0080;

        /// <summary>
        /// Changes the size, position, and Z order of a child, pop-up, or top-level window.
        /// These windows are ordered according to their appearance on the screen.
        /// The topmost window receives the highest rank and is the first window in the Z order.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="hWndInsertAfter">
        ///     A handle to the window to precede the positioned window in the Z order.
        ///     This parameter must be a window handle or one of the following values.
        /// </param>
        /// <param name="X">The new position of the left side of the window, in client coordinates.</param>
        /// <param name="Y">The new position of the top of the window, in client coordinates.</param>
        /// <param name="cx">The new width of the window, in pixels.</param>
        /// <param name="cy">The new height of the window, in pixels.</param>
        /// <param name="uFlags">The window sizing and positioning flags. This parameter can be a combination of the following values.</param>
        /// <returns>true if the function succeeds, otherwise, false.</returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy,
            uint uFlags);

        /// <summary>
        /// Determines whether the specified window is minimized (iconic).
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns>If the window is iconic (minimized), the return value is true, otherwise, false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsIconic(IntPtr hWnd);

        /// <summary>
        /// Determines whether the specified window is maximized (zoomed).
        /// </summary>
        /// <param name="hWnd">A handle to the window to be tested.</param>
        /// <returns>If the window is zoomed (maximized), the return value is true, otherwise, false.</returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsZoomed(IntPtr hWnd);

        /// <summary>
        /// Retrieves the cursor's position, in screen coordinates.
        /// </summary>
        /// <see>See MSDN documentation for further information.</see>
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        // Import the SetCursorPos function from user32.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetCursorPos(int x, int y);

        // Import the GetSystemMetrics function from user32.dll
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(SystemMetric smIndex);

        // System metrics indexes
        public enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        // Callback function for EnumWindows
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        // Import the SetWindowsHookEx function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod,
            uint dwThreadId);

        // Import the UnhookWindowsHookEx function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(IntPtr hhk);

        // Import the CallNextHookEx function from user32.dll
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // Import the GetModuleHandle function from kernel32.dll
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // Import the GetAsyncKeyState function from user32.dll
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        // Keyboard hook procedure
        public delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        // Constants for hook types
        public const int WH_KEYBOARD_LL = 13;
        public const int WM_KEYDOWN = 0x0100;

        /// <summary>
        /// Struct representing a point.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        /// <summary>
        /// Gets the cursor's position.
        /// </summary>
        /// <returns>The cursor's current position.</returns>
        public static Point GetCursorPosition()
        {
            GetCursorPos(out POINT lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            return lpPoint;
        }

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass
        }

        public struct TOKEN_ELEVATION
        {
            public UInt32 TokenIsElevated;
        }

        public const UInt32 TOKEN_QUERY = 0x0008;

        /// <summary>
        /// Retrieves the identifier of the thread that created the specified window and, optionally, the identifier of the process that created the window.
        /// </summary>
        /// <param name="hWnd">A handle to the window.</param>
        /// <param name="lpdwProcessId">
        /// A pointer to a variable that receives the process identifier.
        /// If this parameter is not NULL, GetWindowThreadProcessId copies the identifier of the process to the variable; otherwise, it does not.
        /// If the function fails, the value of the variable is unchanged.</param>
        /// <returns>
        /// If the function succeeds, the return value is the identifier of the thread that created the window.
        /// If the window handle is invalid, the return value is zero. To get extended error information, call GetLastError.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        /// <summary>
        /// Closes an open object handle.
        /// </summary>
        /// <param name="hObject">A valid handle to an open object.</param>
        /// <returns>true if the function succeeds, otherwise, false. To get extended error information, call GetLastError.</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hObject);

        /// <summary>
        /// Opens the access token associated with a process.
        /// </summary>
        /// <param name="ProcessHandle">
        /// A handle to the process whose access token is opened.
        /// The process must have the PROCESS_QUERY_LIMITED_INFORMATION access permission.
        /// </param>
        /// <param name="DesiredAccess">
        /// Specifies an access mask that specifies the requested types of access to the access token.
        /// These requested access types are compared with the discretionary access control list (DACL) of the token to determine which accesses are granted or denied.
        /// </param>
        /// <param name="TokenHandle">A pointer to a handle that identifies the newly opened access token when the function returns.</param>
        /// <returns>If the function succeeds, the return value is true, otherwise, false.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        /// <summary>
        /// The GetTokenInformation function retrieves a specified type of information about an access token.
        /// The calling process must have appropriate access rights to obtain the information.
        /// </summary>
        /// <param name="TokenHandle">
        /// A handle to an access token from which information is retrieved.
        /// If TokenInformationClass specifies TokenSource, the handle must have TOKEN_QUERY_SOURCE access.
        /// For all other TokenInformationClass values, the handle must have TOKEN_QUERY access.
        /// </param>
        /// <param name="TokenInformationClass">
        /// Specifies a value from the TOKEN_INFORMATION_CLASS enumerated type to identify the type of information the function retrieves.
        /// Any callers who check the TokenIsAppContainer and have it return 0 should also verify that the caller token is not an identify level impersonation token.
        /// If the current token is not an app container but is an identity level token, you should return AccessDenied.
        /// </param>
        /// <param name="TokenInformation">
        /// A pointer to a buffer the function fills with the requested information.
        /// The structure put into this buffer depends upon the type of information specified by the TokenInformationClass parameter.
        /// </param>
        /// <param name="TokenInformationLength">
        /// Specifies the size, in bytes, of the buffer pointed to by the TokenInformation parameter.
        /// If TokenInformation is NULL, this parameter must be zero.
        /// </param>
        /// <param name="ReturnLength">
        /// A pointer to a variable that receives the number of bytes needed for the buffer pointed to by the TokenInformation parameter.
        /// If this value is larger than the value specified in the TokenInformationLength parameter, the function fails and stores no data in the buffer.
        /// If the value of the TokenInformationClass parameter is TokenDefaultDacl and the token has no default DACL,
        /// the function sets the variable pointed to by ReturnLength to sizeof(TOKEN_DEFAULT_DACL) and
        /// sets the DefaultDacl member of the TOKEN_DEFAULT_DACL structure to NULL.
        /// </param>
        /// <returns>If the function succeeds, the return value is true, otherwise, false.</returns>
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass,
            out TOKEN_ELEVATION TokenInformation,
            uint TokenInformationLength, out uint ReturnLength);

        /// <summary>
        /// Synthesizes keystrokes, mouse motions, and button clicks.
        /// </summary>
        /// <param name="nInputs">The number of structures in the pInputs array.</param>
        /// <param name="pInputs">
        /// An array of INPUT structures.
        /// Each structure represents an event to be inserted into the keyboard or mouse input stream.
        /// </param>
        /// <param name="cbSize">
        /// The size, in bytes, of an INPUT structure.
        /// If cbSize is not the size of an INPUT structure, the function fails.
        /// </param>
        /// <returns>
        /// The function returns the number of events that it successfully inserted into the keyboard or mouse input stream.
        /// If the function returns zero, the input was already blocked by another thread. To get extended error information, call GetLastError.
        /// <br />
        /// This function fails when it is blocked by UIPI.
        /// Note that neither GetLastError nor the return value will indicate the failure was caused by UIPI blocking.
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        /// <summary>
        /// Retrieves the extra message information for the current thread.
        /// Extra message information is an application- or driver-defined value associated with the current thread's message queue.
        /// </summary>
        /// <returns>
        /// The return value specifies the extra information. The meaning of the extra information is device specific.
        /// </returns>
        [DllImport("user32.dll")]
        private static extern IntPtr GetMessageExtraInfo();

        /// <summary>
        /// Sets the extra message information for the current thread.
        /// Extra message information is an application- or driver-defined value associated with the current thread's message queue.
        /// </summary>
        /// <param name="lParam">The value to be associated with the current thread.</param>
        /// <returns>
        /// The return value is the previous value associated with the current thread.
        /// </returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SetMessageExtraInfo(IntPtr lParam);

        /// <summary>
        /// The INPUT type.
        /// </summary>
        [Flags]
        public enum InputType : uint
        {
            /// <summary>
            /// The event is a mouse event. Use the mi structure of the union.
            /// </summary>
            Mouse = 0,
            /// <summary>
            /// The event is a keyboard event. Use the ki structure of the union.
            /// </summary>
            Keyboard = 1,
            /// <summary>
            /// The event is a hardware event. Use the hi structure of the union.
            /// </summary>
            Hardware = 2
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public InputType Type;
            public InputUnion Data;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)] public MOUSEINPUT Mi;
            [FieldOffset(0)] public KEYBDINPUT Ki;
            [FieldOffset(0)] public HARDWAREINPUT Hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort WVk;
            public ushort WScan;
            public KeyEventF DwFlags;
            public uint Time;
            public IntPtr DwExtraInfo;
        }

        [Flags]
        public enum KeyEventF : uint
        {
            KeyDown = 0x0000,
            ExtendedKey = 0x0001,
            KeyUp = 0x0002,
            Unicode = 0x0004,
            Scancode = 0x0008
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int Dx;
            public int Dy;
            public uint MouseData;
            public uint DwFlags;
            public uint Time;
            public IntPtr DwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HARDWAREINPUT
        {
            public uint UMsg;
            public ushort WParamL;
            public ushort WParamH;
        }
    }
}