using System;
using System.Collections.Generic;
using System.Text;

namespace GiaUI.Data
{
    public struct Color(string name, string color)
    {
        public string Name { get; set; } = name;
        public string ANSI { get; set; } = color;
    }

    public static class TrueColor
    {
        private const string ESC = "\x1b";

        public static string SetForeground(int r, int g, int b) => $"{ESC}[38;2;{r};{g};{b}m";
        public static string SetBackground(int r, int g, int b) => $"{ESC}[48;2;{r};{g};{b}m";
        public static string Reset = $"{ESC}[0m";
    }
}
