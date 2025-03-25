using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ConsoleHelperLibrary.Classes;

// ReSharper disable once CheckNamespace
namespace FactoryConnectionSample;
internal partial class Program
{
    [ModuleInitializer]
    public static void Init()
    {
        AnsiConsole.MarkupLine("");
        Console.Title = "Code sample";
        WindowUtility.SetConsoleWindowPosition(WindowUtility.AnchorWindow.Center);
        //WindowUtility1.MoveWindowToCenter();
    }
}

static class WindowUtility1
{
    [DllImport("user32.dll", SetLastError = true)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

    const uint SWP_NOSIZE = 0x0001;
    const uint SWP_NOZORDER = 0x0004;

    private static Size GetScreenSize() => new Size(GetSystemMetrics(0), GetSystemMetrics(1));

    private struct Size
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }

    [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
    private static extern int GetSystemMetrics(int nIndex);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool GetWindowRect(HandleRef hWnd, out Rect lpRect);

    [StructLayout(LayoutKind.Sequential)]
    private struct Rect
    {
        public int Left;        // x position of upper-left corner
        public int Top;         // y position of upper-left corner
        public int Right;       // x position of lower-right corner
        public int Bottom;      // y position of lower-right corner
    }

    private static Size GetWindowSize(IntPtr window)
    {
        if (!GetWindowRect(new HandleRef(null, window), out Rect rect))
            throw new Exception("Unable to get window rect!");

        int width = rect.Right - rect.Left;
        int height = rect.Bottom - rect.Top;

        return new Size(width, height);
    }

    public static void MoveWindowToCenter()
    {
        IntPtr window = Process.GetCurrentProcess().MainWindowHandle;

        if (window == IntPtr.Zero)
            throw new Exception("Couldn't find a window to center!");

        Size screenSize = GetScreenSize();
        Size windowSize = GetWindowSize(window);

        int x = (screenSize.Width - windowSize.Width) / 2;
        int y = (screenSize.Height - windowSize.Height) / 2;

        SetWindowPos(window, IntPtr.Zero, x, y, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
    }
}
