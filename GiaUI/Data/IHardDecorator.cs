namespace GiaUI.Data
{
    internal interface IHardDecorator
    {
        protected RGB? Rgb { get; set; }
        public bool CanAnimate { get; }
        public double Phase { get; set; }
        public Span<char> CharBuffer { get; }

        public abstract int DecorateOut(Span<char> destination);
        public string DecorateOutBuff() => 
            this.DecorateOut(CharBuffer) == 0 ? string.Empty : new string(CharBuffer);
    }
}
