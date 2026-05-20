using GiaUI.Data;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

#pragma warning disable CS8629

namespace GiaUI.Lib
{
    internal class Shade : IDecorator
    {
        public RGB? Rgb { get; set; }
        public double Phase { get; set; }
        public StringBuilder Buffer { get; } = new();
        public string? Text { get; set; }
        public bool CanAnimate => true;

        private readonly float brightness = 0.5f;

        public Shade(RGB rgb, float _brightness)
        {
            Rgb = rgb;
            brightness = _brightness;
        }

        public string Decorate(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            Buffer.Clear();

            if (Rgb == null)
                throw new Exception("Please choose base color");

            Buffer.EnsureCapacity(text.Length * 20);

            for (int i = 0; i < text.Length; i++)
            {
                double brightness = Math.Cos(0.2 * i + Phase) * 0.4 + 0.6;

                int r = (int)(Rgb?.R * brightness);
                int g = (int)(Rgb?.G * brightness);
                int b = (int)(Rgb?.B * brightness);

                Buffer.Append($"\x1b[38;2;{r};{g};{b}m{text[i]}");
            }
            Buffer.Append("\x1b[0m");
            return Buffer.ToString();
        }
    }
}
