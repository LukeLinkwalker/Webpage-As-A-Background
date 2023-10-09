using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Webpage_As_A_Background.Utils
{
    public static class User32
    {
        [Flags]
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0,
            SMTO_BLOCK = 0x1,
            SMTO_ABORTIFHUNG = 0x2,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x8,
            SMTO_ERRORONEXIT = 0x20
        }

        /// <summary>
        /// Sends the specified message to one or more windows.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is nonzero. SendMessageTimeout does not provide information about individual windows timing out if HWND_BROADCAST is used.
        /// If the function fails or times out, the return value is 0. To get extended error information, call GetLastError.If GetLastError returns ERROR_TIMEOUT, then the function timed out.
        /// </returns>
        /// source: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessagetimeouta
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr hWnd,
            uint Msg,
            IntPtr wParam,
            IntPtr lParam,
            SendMessageTimeoutFlags fuFlags,
            uint uTimeout,
            out IntPtr lpdwResult
            );

        /// <summary>
        /// Copies the text of the specified window's title bar (if it has one) into a buffer. 
        /// If the specified window is a control, the text of the control is copied. 
        /// However, GetWindowText cannot retrieve the text of a control in another application.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is the length, in characters, of the copied string, not including 
        /// the terminating null character. If the window has no title bar or text, if the title bar is empty, or if the 
        /// window or control handle is invalid, the return value is zero. To get extended error information, call GetLastError.
        /// This function cannot retrieve the text of an edit control in another application.
        /// </returns>
        /// source: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtexta
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        /// <summary>
        /// Retrieves the length, in characters, of the specified window's title bar text (if the window has a title bar). 
        /// If the specified window is a control, the function retrieves the length of the text within the control. 
        /// However, GetWindowTextLength cannot retrieve the length of the text of an edit control in another application.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is the length, in characters, of the text. Under certain conditions, this value might be greater than the length of the text (see Remarks).
        /// If the window has no text, the return value is zero.
        /// Function failure is indicated by a return value of zero and a GetLastError result that is nonzero.
        /// </returns>
        /// source: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getwindowtextlengtha
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int GetWindowTextLength(IntPtr hWnd);

        /// <summary>
        /// Retrieves a handle to the top-level window whose class name and window name match the specified strings. 
        /// This function does not search child windows. This function does not perform a case-sensitive search.
        /// To search child windows, beginning with a specified child window, use the FindWindowEx function.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the window that has the specified class name and window name.
        /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        /// source: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowa
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Retrieves a handle to a window whose class name and window name match the specified strings. 
        /// The function searches child windows, beginning with the one following the specified child window. 
        /// This function does not perform a case-sensitive search.
        /// </summary>
        /// <returns>
        /// If the function succeeds, the return value is a handle to the window that has the specified class and window names.
        /// If the function fails, the return value is NULL.To get extended error information, call GetLastError.
        /// </returns>
        /// source: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-findwindowexa
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(
            IntPtr parentHandle, 
            IntPtr hWndChildAfter, 
            string className, 
            string windowTitle
            );

        /// <summary>
        /// Changes the parent window of the specified child window.
        /// </summary>
        /// <returns>If the function succeeds, the return value is a handle to the previous parent window. If the function fails, the return value is NULL.To get extended error information, call GetLastError.</returns>
        /// source: https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setparent
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
    }
}
