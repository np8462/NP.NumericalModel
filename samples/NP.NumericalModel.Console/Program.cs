using NP.NumericalModel;
using NP.NumericalModel.Analysis;
using NP.NumericalModel.ConsoleSample;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NP.NumericalModel.ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            MainForm frmTst = new MainForm();
            Application.Run(frmTst);
        }
    }

    public static class TestConsole
    {
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(
            IntPtr hWnd,
            int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;

        private static bool initialized = false;

        public static void Open()
        {
            if (!initialized)
            {
                AllocConsole();
                initialized = true;
            }

            IntPtr handle = GetConsoleWindow();

            if (handle != IntPtr.Zero)
            {
                ShowWindow(handle, SW_SHOW);
            }

            Console.Clear();
        }

        public static void Hide()
        {
            IntPtr handle = GetConsoleWindow();

            if (handle != IntPtr.Zero)
            {
                ShowWindow(handle, SW_HIDE);
            }
        }
    }
}