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

        List<IDecorator> decs = new();
        for (int i = 0; i < 20; i++)
        {
            decs.Add(new Shade(new(Random.Shared.Next(10, 255), Random.Shared.Next(0, 255), Random.Shared.Next(30, 255)), 1f));
            decs[i].Text = Random.Shared.Next(100000, 200000).ToString();
        }

        Animation animation = new(20, decs.ToArray());
        animation.Start();

        Console.ReadKey();
    }
}