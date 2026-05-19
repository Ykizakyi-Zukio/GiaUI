using GiaUI.Data;
using System.Text;

namespace GiaUI.Lib
{
    internal class Rainbow : IDecorator
    {
        public RGB? Rgb { get; set; }
        public double Phase { get; set; }
        public StringBuilder Buffer => new();
        public string? Text { get; set; }

        public bool CanAnimate => true;

        private float sinAngle = 0.3f;
        private const byte upper = 2;

        public Rainbow() { }
        public Rainbow(float _sinAngle, double _phase)
        {
            sinAngle = _sinAngle;
            Phase = _phase;
        }

        public async Task<string> Decorate(string text)
        {
            
            if (string.IsNullOrEmpty(text)) throw new Exception("Set text");
            Buffer.Clear();

            for (int i = 0; i < text.Length; i++)
            {
                int r = (int)(Math.Sin(sinAngle * i + Phase + 0) * 127 + 128);
                int g = (int)(Math.Sin(sinAngle * i + Phase + upper) * 127 + 128);
                int b = (int)(Math.Sin(sinAngle * i + Phase + upper * 2) * 127 + 128);

                Buffer.Append($"\x1b[38;2;{r};{g};{b}m{text[i]}");
            }

            Buffer.Append("\x1b[0m");
            Phase += 0.1D;
            return Buffer.ToString();
        }
    }
}
