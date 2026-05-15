using System.Runtime.InteropServices;

namespace GiaUI.Marshal;
public static class ConsoleHelper
{
    private const int STD_OUTPUT_HANDLE = -11;
    private const uint ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll")]
    private static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

    [DllImport("kernel32.dll")]
    private static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);

    public static void EnableAnsi()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var iStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
            if (GetConsoleMode(iStdOut, out uint outMode))
            {
                SetConsoleMode(iStdOut, outMode | ENABLE_VIRTUAL_TERMINAL_PROCESSING);
            }
        }
    }
}