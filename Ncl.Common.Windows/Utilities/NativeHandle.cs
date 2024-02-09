using System;

namespace Ncl.Common.Windows.Utilities
{
    /// <summary>
    /// A class that wraps a native handle and an action to dispose of the native resource.
    /// </summary>
    public class NativeHandle : IDisposable
    {
        private readonly Action<IntPtr> _disposeAction;

        /// <summary>
        /// A class that wraps a native handle and an action to dispose of the native resource.
        /// </summary>
        /// <param name="handle">The native handle.</param>
        /// <param name="disposeAction">The action to call to dispose of the native resource when disposed.</param>
        public NativeHandle(IntPtr handle, Action<IntPtr> disposeAction)
        {
            Handle = handle;
            _disposeAction = disposeAction;
        }

        /// <summary>
        /// The handle to the native resource.
        /// </summary>
        public IntPtr Handle { get; private set; }

        /// <summary>
        /// Has the handle been closed?
        /// </summary>
        public bool IsClosed => Handle == IntPtr.Zero;

        /// <summary>
        /// The action to dispose of the native resource.
        /// </summary>
        public void Dispose()
        {
            if (Handle == IntPtr.Zero) 
                return;

            _disposeAction.Invoke(Handle);
            Handle = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }
    }
}
