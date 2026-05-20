using GiaUI.Data;
using System.Text;
using Cysharp.Text;

namespace GiaUI.Lib
{
    internal class Rainbow : IDecorator
    {
        public RGB? Rgb { get; set; }
        public double Phase { get; set; } = 3D;

        public StringBuilder Buffer { get; } = new();
        public string? Text { get; set; }

        public bool CanAnimate => true;

        private float sinAngle;
        private const byte upper = 2;

        public Rainbow(float _sinAngle = 0.3f, double _phase = 0)
        {
            sinAngle = _sinAngle;
            Phase = _phase;
        }

        public string Decorate(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            Buffer.Clear();

            Buffer.EnsureCapacity(text.Length * 20);

            for (int i = 0; i < text.Length; i++)
            {
                int r = (int)(Math.Sin(sinAngle * i + Phase + 0) * 127 + 128);
                int g = (int)(Math.Sin(sinAngle * i + Phase + upper) * 127 + 128);
                int b = (int)(Math.Sin(sinAngle * i + Phase + upper * 2) * 127 + 128);

                Buffer.Append($"\x1b[38;2;{r};{g};{b}m{text[i]}");
            }

            Buffer.Append("\x1b[0m");

            return Buffer.ToString();
        }

        public string Decorate() => Decorate(Text ?? throw new InvalidOperationException("Text property is not set."));
    }
}