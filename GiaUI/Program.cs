using GiaUI.Marshal;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8; // Включаем поддержку UTF-8 для символов
        ConsoleHelper.EnableAnsi();
        Console.CursorVisible = false; // Скрываем курсор, чтобы он не бегал

        string text = "\r\n\r\n\r\n░░░░░░░░░░░░░░░▄▀▄░░░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░▄▀░░░▀▄░░░░░░░░░░░░░\r\n░░░░░░░░░░░▄▀░░░░▄▀█░░░░░░░░░░░░░\r\n░░░░░░░░░▄▀░░░░▄▀░▄▀░▄▀▄░░░░░░░░░\r\n░░░░░░░▄▀░░░░▄▀░▄▀░▄▀░░░▀▄░░░░░░░\r\n░░░░░░░█▀▄░░░░▀█░▄▀░░░░░░░▀▄░░░░░\r\n░░░▄▀▄░▀▄░▀▄░░░░▀░░░░▄█▄░░░░▀▄░░░\r\n░▄▀░░░▀▄░▀▄░▀▄░░░░░▄▀░█░▀▄░░░░▀▄░\r\n░█▀▄░░░░▀▄░█▀░░░░░░░▀█░▀▄░▀▄░▄▀█░\r\n░▀▄░▀▄░░░░▀░░░░▄█▄░░░░▀▄░▀▄░█░▄▀░\r\n░░░▀▄░▀▄░░░░░▄▀░█░▀▄░░░░▀▄░▀█▀░░░\r\n░░░░░▀▄░▀▄░▄▀░▄▀░█▀░░░░▄▀█░░░░░░░\r\n░░░░░░░▀▄░█░▄▀░▄▀░░░░▄▀░▄▀░░░░░░░\r\n░░░░░░░░░▀█▀░▄▀░░░░▄▀░▄▀░░░░░░░░░\r\n░░░░░░░░░░░░░█▀▄░▄▀░▄▀░░░░░░░░░░░\r\n░░░░░░░░░░░░░▀▄░█░▄▀░░░░░░░░░░░░░\r\n░░░░░░░░░░░░░░░▀█▀░░░░░░░░░░░░░░░\r\n\r\n";

        Animation rnbw = new Animation(20, AnimationType.Shade, 0, 0);
        rnbw.Text = text;
        rnbw.BaseColor = new(86, 168, 214);
        rnbw.Start();

        Console.WriteLine();
        Animation raduga = new Animation(20, AnimationType.Rainbow, 25, 0);
        raduga.Text = text;
        raduga.Start();

        Thread.Sleep(1000);

        rnbw.Stop();
        raduga.Stop();
        //Console.WriteLine();
        Console.ReadLine();
        //rnbw.PosY -= 1;
        Console.Clear();
        rnbw.Start();
        raduga.Start();
        Console.ReadKey();
    }
}