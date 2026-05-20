using System;
using System.Collections.Generic;
using System.Text;

namespace GiaUI.Data
{
    public class TableStyle
    {
        public char TopLeft { get; init; } = '┌';
        public char TopRight { get; init; } = '┐';
        public char BottomLeft { get; init; } = '└';
        public char BottomRight { get; init; } = '┘';
        public char Horizontal { get; init; } = '─';
        public char Vertical { get; init; } = '│';
        public char Cross { get; init; } = '┼';
        public char TTop { get; init; } = '┬';
        public char TBottom { get; init; } = '┴';
        public char TLeft { get; init; } = '├';
        public char TRight { get; init; } = '┤';

        public static TableStyle Modern => new();
        public static TableStyle Double => new() { TopLeft = '╔', TopRight = '╗', BottomLeft = '╚', BottomRight = '╝', Horizontal = '═', Vertical = '║', Cross = '╬', TTop = '╦', TBottom = '╩', TLeft = '╠', TRight = '╣' };
        public static TableStyle Ascii => new() { TopLeft = '+', TopRight = '+', BottomLeft = '+', BottomRight = '+', Horizontal = '-', Vertical = '|', Cross = '+', TTop = '+', TBottom = '+', TLeft = '+', TRight = '+' };
        public static TableStyle Rounded => new() { TopLeft = '╭', TopRight = '╮', BottomLeft = '╰', BottomRight = '╯', Horizontal = '─', Vertical = '│', Cross = '┼', TTop = '┬', TBottom = '┴', TLeft = '├', TRight = '┤' };
    }
}
