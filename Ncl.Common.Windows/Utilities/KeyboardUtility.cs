using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Ncl.Common.Windows.Native;

namespace Ncl.Common.Windows.Utilities
{
    /// <summary>
    /// Utility class for handling keyboard input.
    /// </summary>
    public static class KeyboardUtility
    {
        // Constants for virtual key codes
        public const int VkShift = 0x10;
        public const int VkControl = 0x11;
        public const int VkAlt = 0x12;
        public const int VkEscape = 0x1B;
        public const int VkSpace = 0x20;

        public delegate void OnKeyPress(int keyPressed, bool shiftHeld, bool ctrlHeld, bool altHeld);

        // Handle for the keyboard hook
        private static IntPtr _keyboardHookHandle = IntPtr.Zero;

        private static readonly NativeMethods.LowLevelKeyboardProc KeyboardHookCallbackDelegate = KeyboardHookCallback;
        private static OnKeyPress _keyPressCallback;

        /// <summary>
        /// Start the keyboard hook.
        /// </summary>
        /// <param name="keyPressCallback">The callback for when a key is pressed.</param>
        /// <exception cref="ArgumentNullException">The callback is null.</exception>
        /// <exception cref="InvalidOperationException">Can not get the current process module.</exception>
        /// <exception cref="Win32Exception">A win32 exception occurs.</exception>
        public static void StartKeyboardHook(OnKeyPress keyPressCallback)
        {
            _keyPressCallback = keyPressCallback ?? throw new ArgumentNullException(nameof(keyPressCallback));

            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                if (curModule == null)
                    throw new InvalidOperationException("Could not get the current process module");

                _keyboardHookHandle = NativeMethods.SetWindowsHookEx(NativeMethods.WH_KEYBOARD_LL,
                                       KeyboardHookCallbackDelegate,
                                       NativeMethods.GetModuleHandle(curModule.ModuleName), 0);

                if (_keyboardHookHandle != IntPtr.Zero)
                    return;

                throw new Win32Exception(Marshal.GetLastWin32Error());
            }
        }

        /// <summary>
        /// Stop the keyboard hook.
        /// </summary>
        public static void StopKeyboardHook()
        {
            if (_keyboardHookHandle == IntPtr.Zero)
                return;

            _keyPressCallback = null;

            NativeMethods.UnhookWindowsHookEx(_keyboardHookHandle);
            _keyboardHookHandle = IntPtr.Zero;
        }

        // Keyboard hook callback
        private static IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0 || wParam != (IntPtr)NativeMethods.WM_KEYDOWN)
                return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);

            int vkCode = Marshal.ReadInt32(lParam);
            bool shift = (NativeMethods.GetAsyncKeyState(VkShift) & 0x8000) != 0;
            bool alt = (NativeMethods.GetAsyncKeyState(VkAlt) & 0x8000) != 0;
            bool ctrl = (NativeMethods.GetAsyncKeyState(VkControl) & 0x8000) != 0;

            _keyPressCallback?.Invoke(vkCode, shift, ctrl, alt);
            return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
    }
}