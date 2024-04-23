using System.Drawing;
using Ncl.Common.Windows.Native;

namespace Ncl.Common.Windows.Utilities
{
    /// <summary>
    /// Utility class for mouse operations.
    /// </summary>
    public static class MouseUtility
    {
        /// <summary>
        /// Gets the cursor's position.
        /// </summary>
        /// <returns>The cursor's current position.</returns>
        public static Point GetCursorPosition()
        {
            NativeMethods.GetCursorPos(out NativeMethods.POINT lpPoint);
            // NOTE: If you need error handling
            // bool success = GetCursorPos(out lpPoint);
            // if (!success)

            // ReSharper disable once RedundantCast
            return (Point)lpPoint;
        }
    }
}
