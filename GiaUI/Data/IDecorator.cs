namespace GiaUI.Data
{
    public interface IDecorator
    {
        protected RGB? Rgb { get; set; }
        public bool CanAnimate { get; }
        public double Phase { get; set; }
        public string? Text { get; set; }

        public abstract string Decorate(string text);
        public string Decorate() => Decorate(Text ?? throw new Exception("Set text"));

        public char[] Decorate(char[] chars);
    }
}
