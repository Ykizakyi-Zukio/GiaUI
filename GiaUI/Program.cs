using GiaUI.Data;
using GiaUI.Lib;
using GiaUI.Marshal;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        ConsoleHelper.EnableAnsi();

        Rainbow rnbw = new() { Text = "Hello world!"};
        Shade shade = new( new(140, 30, 67), 0.6f) { Text = "Hello world, again.." };

        Animation animation = new(20, [rnbw, shade]);
        animation.Start();
    }
}