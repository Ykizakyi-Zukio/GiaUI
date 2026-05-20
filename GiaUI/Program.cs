using GiaUI.Data;
using GiaUI.Lib;
using GiaUI.Marshal;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.CursorVisible = false;
        ConsoleHelper.EnableAnsi();

        Rainbow rnbw = new(0.5f, 2) { Text = "Hello world!"};
        Shade shade = new( new(140, 30, 67), 0.6f) { Text = "Hello world, again.." };

        Animation animation = new(20, [rnbw]);
        Console.Write(rnbw.Decorate("Hello worlddddddd") + "HELLO");
        //animation.Start();

        StringBuilder Buffer = new();
        float sinAngle = 0.3f;
        byte upper = 2;
        double Phase = 0;
        string text = "Hello world";

        for (int i = 0; i < text.Length; i++)
        {
            int r = (int)(Math.Sin(sinAngle * i + Phase + 0) * 127 + 128);
            int g = (int)(Math.Sin(sinAngle * i + Phase + upper) * 127 + 128);
            int b = (int)(Math.Sin(sinAngle * i + Phase + upper * 2) * 127 + 128);

            Buffer.Append($"\x1b[38;2;{r};{g};{b}m{text[i]}");
        }

        Buffer.Append("\x1b[0m");
        Console.WriteLine(Buffer.ToString());

        Console.ReadKey();
    }
}