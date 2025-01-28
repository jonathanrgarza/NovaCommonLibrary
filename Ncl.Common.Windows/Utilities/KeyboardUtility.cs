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
    public class KeyboardUtility
    {
        /// <summary>
        /// The delegate for the hook status changed callback.
        /// </summary>
        /// <param name="hooked">The status of the hook.</param>
        public delegate void HookStatusChangedHandler(bool hooked);

        /// <summary>
        /// The delegate for the key down callback.
        /// </summary>
        /// <param name="keyPressed">The virtual key code for the key.</param>
        /// <param name="shiftHeld">The pressed status of the shift key.</param>
        /// <param name="ctrlHeld">The pressed status of the ctrl key.</param>
        /// <param name="altHeld">The pressed status of the alt key.</param>
        public delegate void KeyDownHandler(VirtualKeyCodes keyPressed, bool shiftHeld, bool ctrlHeld, bool altHeld);

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

        private static KeyboardUtility _instance;

        private readonly NativeMethods.LowLevelKeyboardProc _keyboardHookCallbackDelegate;

        // Handle for the keyboard hook
        private IntPtr _keyboardHookHandle = IntPtr.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardUtility"/> class.
        /// </summary>
        private KeyboardUtility()
        {
            _keyboardHookCallbackDelegate = KeyboardHookCallback;
        }

        /// <summary>
        /// Gets the instance of the <see cref="KeyboardUtility"/>.
        /// </summary>
        public static KeyboardUtility Instance => _instance ?? (_instance = new KeyboardUtility());

        /// <summary>
        /// Gets a value indicating whether the keyboard hook is active.
        /// </summary>
        public bool IsHooked => _keyboardHookHandle != IntPtr.Zero;

        /// <summary>
        /// Occurs when the keyboard hook status changes.
        /// </summary>
        public event HookStatusChangedHandler HookStatusChanged;

        /// <summary>
        /// Occurs when a key is down.
        /// </summary>
        public event KeyDownHandler KeyDown;

        /// <summary>
        /// Start the keyboard hook.
        /// </summary>
        /// <exception cref="InvalidOperationException">Can not get the current process module.</exception>
        /// <exception cref="Win32Exception">A win32 exception occurs.</exception>
        public void StartKeyboardHook()
        {
            if (_keyboardHookHandle != IntPtr.Zero)
            {
                StopKeyboardHook();
            }

            using (var curProcess = Process.GetCurrentProcess())
            using (var curModule = curProcess.MainModule)
            {
                if (curModule == null)
                {
                    throw new InvalidOperationException("Could not get the current process module");
                }

                _keyboardHookHandle = NativeMethods.SetWindowsHookEx(NativeMethods.WH_KEYBOARD_LL,
                    _keyboardHookCallbackDelegate,
                    NativeMethods.GetModuleHandle(curModule.ModuleName), 0);

                if (_keyboardHookHandle == IntPtr.Zero)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                HookStatusChanged?.Invoke(true);
            }
        }

        /// <summary>
        /// Stop the keyboard hook.
        /// </summary>
        public void StopKeyboardHook()
        {
            if (_keyboardHookHandle == IntPtr.Zero)
            {
                return;
            }

            NativeMethods.UnhookWindowsHookEx(_keyboardHookHandle);
            _keyboardHookHandle = IntPtr.Zero;

            HookStatusChanged?.Invoke(false);
        }

        /// <summary>
        /// The callback for the keyboard hook.
        /// </summary>
        /// <param name="nCode">Specifies a code the hook procedure uses to determine how to process the message.</param>
        /// <param name="wParam">Specifies mouse message.</param>
        /// <param name="lParam">Specifies message.</param>
        /// <returns></returns>
        private IntPtr KeyboardHookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode < 0 || wParam != (IntPtr)NativeMethods.WM_KEYDOWN)
            {
                return NativeMethods.CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
            }

            int vkCode = Marshal.ReadInt32(lParam);
            var key = (VirtualKeyCodes)vkCode;
            bool shift = (NativeMethods.GetAsyncKeyState(VkShift) & 0x8000) != 0;
            bool ctrl = (NativeMethods.GetAsyncKeyState(VkControl) & 0x8000) != 0;
            bool alt = (NativeMethods.GetAsyncKeyState(VkAlt) & 0x8000) != 0;
            //Debug.WriteLine($"Key: {key}, Shift: {shift}, Ctrl: {ctrl}, Alt: {alt}");
            KeyDown?.Invoke(key, shift, ctrl, alt);
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