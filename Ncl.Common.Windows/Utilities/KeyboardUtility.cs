using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using Ncl.Common.Windows.Infrastructure;
using Ncl.Common.Windows.Native;

namespace Ncl.Common.Windows.Utilities
{
    /// <summary>
    /// Utility class for handling keyboard input.
    /// </summary>
    public static class KeyboardUtility
    {
        // Constants for virtual key codes
        // https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
        /// <summary>
        /// The virtual key code for the shift key.
        /// </summary>
        public const int VkShift = 0x10;
        /// <summary>
        /// The virtual key code for the ctrl key.
        /// </summary>
        public const int VkControl = 0x11;
        /// <summary>
        /// The virtual key code for the alt key.
        /// </summary>
        public const int VkAlt = 0x12;

        /// <summary>
        /// The delegate for the key press callback.
        /// </summary>
        /// <param name="keyPressed">The virtual key code for the key pressed.</param>
        /// <param name="shiftHeld">The pressed status of the shift key.</param>
        /// <param name="ctrlHeld">The pressed status of the ctrl key.</param>
        /// <param name="altHeld">The pressed status of the alt key.</param>
        public delegate void OnKeyPress(VirtualKeyCodes keyPressed, bool shiftHeld, bool ctrlHeld, bool altHeld);

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

            if (_keyboardHookHandle != IntPtr.Zero)
            {
                StopKeyboardHook();
            }

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
            var key = (VirtualKeyCodes)vkCode;
            bool shift = (NativeMethods.GetAsyncKeyState(VkShift) & 0x8000) != 0;
            bool ctrl = (NativeMethods.GetAsyncKeyState(VkControl) & 0x8000) != 0;
            bool alt = (NativeMethods.GetAsyncKeyState(VkAlt) & 0x8000) != 0;
            //Debug.WriteLine($"Key: {key}, Shift: {shift}, Ctrl: {ctrl}, Alt: {alt}");
            _keyPressCallback?.Invoke(key, shift, ctrl, alt);
            return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        /// <summary>
        /// Simulates a key press.
        /// </summary>
        /// <param name="keyCode">The virtual key code of the key to press.</param>
        /// <exception cref="Win32Exception">A Win32 exception occurs.</exception>
        public static void SendKeyPress(VirtualKeyCodes keyCode)
        {
            var inputs = new[]
            {
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard,
                    Data = new NativeMethods.InputUnion
                    {
                        Ki = new NativeMethods.KEYBDINPUT
                        {
                            WVk = (ushort)keyCode,
                            WScan = 0,
                            DwFlags = NativeMethods.KeyEventF.KeyDown
                        }
                    }
                },
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard,
                    Data = new NativeMethods.InputUnion
                    {
                        Ki = new NativeMethods.KEYBDINPUT
                        {
                            WVk = (ushort)keyCode,
                            WScan = 0,
                            DwFlags = NativeMethods.KeyEventF.KeyUp
                        }
                    }
                }
            };

            int inputSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
            uint result = NativeMethods.SendInput((uint)inputs.Length, inputs, inputSize);

            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Simulates key presses for a list of keys (simultaneously).
        /// </summary>
        /// <param name="keyCodes">The virtual key codes of the keys to press.</param>
        /// <exception cref="Win32Exception">A Win32 exception occurs.</exception>
        public static void SendKeyPress(IEnumerable<VirtualKeyCodes> keyCodes)
        {
            var virtualKeyCodesEnumerable = keyCodes as VirtualKeyCodes[] ?? keyCodes.ToArray();
            var inputList = virtualKeyCodesEnumerable.Select(key => 
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard, 
                    Data = 
                        new NativeMethods.InputUnion
                        {
                            Ki = new NativeMethods.KEYBDINPUT
                            {
                                WVk = (ushort)key, 
                                WScan = 0, 
                                DwFlags = NativeMethods.KeyEventF.KeyDown
                            }
                        }
                }).ToList();
            inputList.AddRange(virtualKeyCodesEnumerable.Select(key => 
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard, 
                    Data = new NativeMethods.InputUnion
                    {
                        Ki = new NativeMethods.KEYBDINPUT
                        {
                            WVk = (ushort)key,
                            WScan = 0, 
                            DwFlags = NativeMethods.KeyEventF.KeyUp
                        }
                    }
                }));

            var inputs = inputList.ToArray();
            int inputSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
            uint result = NativeMethods.SendInput((uint)inputs.Length, inputs, inputSize);

            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Simulates key presses for a list of keys (independently).
        /// </summary>
        /// <param name="keyCodes">The virtual key codes of the keys to press.</param>
        /// <exception cref="Win32Exception">A Win32 exception occurs.</exception>
        public static void SendKeyPressIndependent(IEnumerable<VirtualKeyCodes> keyCodes)
        {
            var inputList = new List<NativeMethods.INPUT>();
            foreach (var key in keyCodes)
            {
                inputList.Add(new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard,
                    Data = new NativeMethods.InputUnion
                    {
                        Ki = new NativeMethods.KEYBDINPUT
                        {
                            WVk = (ushort)key,
                            WScan = 0,
                            DwFlags = NativeMethods.KeyEventF.KeyDown
                        }
                    }
                });
                inputList.Add(new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard,
                    Data = new NativeMethods.InputUnion
                    {
                        Ki = new NativeMethods.KEYBDINPUT
                        {
                            WVk = (ushort)key,
                            WScan = 0,
                            DwFlags = NativeMethods.KeyEventF.KeyUp
                        }
                    }
                });
            }

            var inputs = inputList.ToArray();
            int inputSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
            uint result = NativeMethods.SendInput((uint)inputs.Length, inputs, inputSize);

            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Simulates a key down event.
        /// </summary>
        /// <param name="keyCode">The virtual key code of the key to press.</param>
        /// <exception cref="Win32Exception">A Win32 exception occurs.</exception>
        public static void SendKeyDown(VirtualKeyCodes keyCode)
        {
            var inputs = new[]
            {
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard,
                    Data = new NativeMethods.InputUnion
                    {
                        Ki = new NativeMethods.KEYBDINPUT
                        {
                            WVk = (ushort)keyCode,
                            WScan = 0,
                            DwFlags = NativeMethods.KeyEventF.KeyDown
                        }
                    }
                }
            };

            int inputSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
            uint result = NativeMethods.SendInput((uint)inputs.Length, inputs, inputSize);

            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Simulates key down event for a list of keys.
        /// </summary>
        /// <param name="keyCodes">The virtual key codes of the keys to press.</param>
        /// <exception cref="Win32Exception">A Win32 exception occurs.</exception>
        public static void SendKeyDown(IEnumerable<VirtualKeyCodes> keyCodes)
        {
            var inputs = keyCodes.Select(key => 
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard, 
                    Data = new NativeMethods.InputUnion
                        {
                            Ki = new NativeMethods.KEYBDINPUT
                            {
                                WVk = (ushort)key, 
                                WScan = 0, 
                                DwFlags = NativeMethods.KeyEventF.KeyDown
                            }
                        }
                }).ToArray();
            int inputSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
            uint result = NativeMethods.SendInput((uint)inputs.Length, inputs, inputSize);

            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Simulates a key up event.
        /// </summary>
        /// <param name="keyCode">The virtual key code of the key to press.</param>
        /// <exception cref="Win32Exception">A Win32 exception occurs.</exception>
        public static void SendKeyUp(VirtualKeyCodes keyCode)
        {
            var inputs = new[]
            {
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard,
                    Data = new NativeMethods.InputUnion
                    {
                        Ki = new NativeMethods.KEYBDINPUT
                        {
                            WVk = (ushort)keyCode,
                            WScan = 0,
                            DwFlags = NativeMethods.KeyEventF.KeyUp
                        }
                    }
                }
            };

            int inputSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
            uint result = NativeMethods.SendInput((uint)inputs.Length, inputs, inputSize);

            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }

        /// <summary>
        /// Simulates key up event for a list of keys.
        /// </summary>
        /// <param name="keyCodes">The virtual key codes of the keys to press.</param>
        /// <exception cref="Win32Exception">A Win32 exception occurs.</exception>
        public static void SendKeyUp(IEnumerable<VirtualKeyCodes> keyCodes)
        {
            var inputs = keyCodes.Select(key => 
                new NativeMethods.INPUT
                {
                    Type = NativeMethods.InputType.Keyboard, 
                    Data = new NativeMethods.InputUnion
                        {
                            Ki = new NativeMethods.KEYBDINPUT
                            {
                                WVk = (ushort)key, 
                                WScan = 0, 
                                DwFlags = NativeMethods.KeyEventF.KeyUp
                            }
                        }
                }).ToArray();
            int inputSize = Marshal.SizeOf(typeof(NativeMethods.INPUT));
            uint result = NativeMethods.SendInput((uint)inputs.Length, inputs, inputSize);

            if (result == 0)
                throw new Win32Exception(Marshal.GetLastWin32Error());
        }
    }
}