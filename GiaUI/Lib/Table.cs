using System;
using System.Buffers;
using System.Collections.Generic;
using GiaUI.Data;

namespace GiaUI.Lib
{
    public enum TextAlignment { Left, Center, Right }

    public class ColumnDefinition
    {
        public string Header { get; }
        public TextAlignment Alignment { get; set; } = TextAlignment.Left;
        public string? Format { get; set; }

        public ColumnDefinition(string header) => Header = header;
    }

    public class Table
    {
        private readonly List<ColumnDefinition> _columns = new();
        private readonly List<object[]> _rows = new();
        public TableStyle Style { get; set; } = TableStyle.Modern;
        public int Padding { get; set; } = 1;

        public void AddColumn(string header, TextAlignment alignment = TextAlignment.Left, string format = null)
        {
            _columns.Add(new ColumnDefinition(header) { Alignment = alignment, Format = format });
        }

        public void AddRow(params object[] values)
        {
            if (values.Length != _columns.Count)
                throw new ArgumentException("Data rows count don`t match with colums");
            _rows.Add(values);
        }

        public void Print(IDecorator? dec = null)
        {
            int colCount = _columns.Count;
            int[] widths = ArrayPool<int>.Shared.Rent(colCount);

            try
            {
                for (int i = 0; i < colCount; i++)
                {
                    widths[i] = _columns[i].Header.Length;
                }

                for (int r = 0; r < _rows.Count; r++)
                {
                    for (int c = 0; c < colCount; c++)
                    {
                        int len = GetValueStringLength(_rows[r][c], _columns[c].Format);
                        if (len > widths[c]) widths[c] = len;
                    }
                }

                for (int i = 0; i < colCount; i++) widths[i] += Padding * 2;

                int totalRowWidth = colCount + 1;
                for (int i = 0; i < colCount; i++) totalRowWidth += widths[i];

                char[] buffer = ArrayPool<char>.Shared.Rent(totalRowWidth + 2);

                try
                {
                    // UP
                    PrintSeparator(buffer, widths, Style.TopLeft, Style.TTop, Style.TopRight, Style.Horizontal, ref dec);

                    // Headers
                    PrintHeaderRow(buffer, widths, ref dec);

                    // Headers separator
                    PrintSeparator(buffer, widths, Style.TLeft, Style.Cross, Style.TRight, Style.Horizontal, ref dec);

                    // Data rows
                    for (int i = 0; i < _rows.Count; i++)
                    {
                        PrintDataRow(buffer, widths, _rows[i], ref dec);
                    }

                    // DOWN
                    PrintSeparator(buffer, widths, Style.BottomLeft, Style.TBottom, Style.BottomRight, Style.Horizontal, ref dec);
                }
                finally
                {
                    ArrayPool<char>.Shared.Return(buffer);
                }
            }
            finally
            {
                ArrayPool<int>.Shared.Return(widths);
            }
        }

        private int GetValueStringLength(object value, string format)
        {
            if (value is null) return 0;
            if (value is IFormattable formattable)
                return formattable.ToString(format, null).Length;
            return value.ToString().Length;
        }

        private void PrintSeparator(char[] buf, int[] widths, char left, char mid, char right, char hor, ref IDecorator? dec)
        {
            int idx = 0;
            buf[idx++] = left;
            for (int i = 0; i < _columns.Count; i++)
            {
                for (int j = 0; j < widths[i]; j++) buf[idx++] = hor;
                buf[idx++] = (i == _columns.Count - 1) ? right : mid;
            }

            if (dec == null)
                Console.WriteLine(buf, 0, idx);
            else
                Console.WriteLine(dec.Decorate(buf), 0, idx);
        }

        private void PrintHeaderRow(char[] buf, int[] widths, ref IDecorator? dec)
        {
            int idx = 0;
            buf[idx++] = Style.Vertical;
            for (int i = 0; i < _columns.Count; i++)
            {
                WriteAligned(buf, ref idx, _columns[i].Header, widths[i], _columns[i].Alignment);
                buf[idx++] = Style.Vertical;
            }

            if (dec == null)
                Console.WriteLine(buf, 0, idx);
            else
                Console.WriteLine(dec.Decorate(buf), 0, idx);
        }

        private void PrintDataRow(char[] buf, int[] widths, object[] row, ref IDecorator? dec)
        {
            int idx = 0;
            buf[idx++] = Style.Vertical;
            for (int i = 0; i < _columns.Count; i++)
            {
                string str = row[i] is IFormattable formattable
                    ? formattable.ToString(_columns[i].Format, null)
                    : row[i]?.ToString() ?? string.Empty;

                WriteAligned(buf, ref idx, str, widths[i], _columns[i].Alignment);
                buf[idx++] = Style.Vertical;
            }

            if (dec == null)
                Console.WriteLine(buf, 0, idx);
            else
                Console.WriteLine(dec.Decorate(buf), 0, idx);
        }

        private void WriteAligned(char[] buf, ref int idx, string text, int width, TextAlignment alignment)
        {
            int textLen = text.Length;
            int totalSpaces = width - textLen;
            int leftSpaces = 0;
            int rightSpaces = 0;

            switch (alignment)
            {
                case TextAlignment.Left:
                    leftSpaces = Padding;
                    rightSpaces = totalSpaces - Padding;
                    break;
                case TextAlignment.Right:
                    leftSpaces = totalSpaces - Padding;
                    rightSpaces = Padding;
                    break;
                case TextAlignment.Center:
                    leftSpaces = totalSpaces / 2;
                    rightSpaces = totalSpaces - leftSpaces;
                    break;
            }

            for (int i = 0; i < leftSpaces; i++) buf[idx++] = ' ';
            for (int i = 0; i < textLen; i++) buf[idx++] = text[i];
            for (int i = 0; i < rightSpaces; i++) buf[idx++] = ' ';
        }
    }
}
