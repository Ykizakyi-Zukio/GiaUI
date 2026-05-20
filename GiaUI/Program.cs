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

        var table = new Table() { Style = TableStyle.Rounded, Padding = 2 };
        Rainbow rnbw = new();

        table.AddColumn("Name");
        table.AddColumn("Age");

        table.AddRow("George", 45);
        table.AddRow("Alice", 30);

        table.Print(rnbw);

        char[] chars = "Hello, World!".ToCharArray();
        Console.WriteLine(rnbw.Decorate(chars));

        Console.ReadKey();
    }
}