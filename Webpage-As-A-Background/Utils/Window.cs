using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Webpage_As_A_Background.Utils.User32;

namespace Webpage_As_A_Background.Utils
{
    public static class Window
    {
        private static bool isFirstMove = true;

        /// <summary>
        /// Moves the window in the z-order such that the window is immediatly in 
        /// front of the desktop background but behind the desktop icons.
        /// </summary>
        /// <param name="targetHandle"></param>
        public static void Move(IntPtr targetHandle)
        {
            // Progman <-> Program Manager (Same window)

            // Sending a message to Progman in order to spawn a window between the desktop background and icons.
            if(isFirstMove == true)
            {
                IntPtr progman = User32.FindWindow("Progman", null);
                IntPtr result = IntPtr.Zero;

                SendMessageTimeout(progman,
                       0x052C,
                       IntPtr.Zero,
                       IntPtr.Zero,
                       SendMessageTimeoutFlags.SMTO_NORMAL,
                       1000,
                       out result);

                isFirstMove = true;
            }

            // Looping through windows to find the immediatly in front of the Progman window
            IntPtr currentWindowHandle = User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, null, null);
            while (currentWindowHandle != IntPtr.Zero)
            {
                IntPtr nextWindowHandle = User32.FindWindowEx(IntPtr.Zero, currentWindowHandle, null, null);

                if (GetWindowText(nextWindowHandle) == "Program Manager")
                {
                    break;
                }

                currentWindowHandle = nextWindowHandle;
            }

            // Updating the parent of 
            User32.SetParent(targetHandle, currentWindowHandle);
        }

        public static string GetWindowText(IntPtr WindowHandle)
        {
            int windowTextLength = User32.GetWindowTextLength(WindowHandle) + 1;
            StringBuilder SB = new StringBuilder(windowTextLength);
            User32.GetWindowText(WindowHandle, SB, windowTextLength);
            return SB.ToString();
        }
    }
}
