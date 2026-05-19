namespace GiaUI.Data
{
    public interface IDecorator
    {
        protected RGB? Rgb { get; set; }
        public bool CanAnimate { get; }
        public double Phase { get; set; }
        public string? Text { get; set; }

        public abstract Task<string> Decorate(string text);
        public async Task<string> Decorate() => await Decorate(Text ?? throw new Exception("Set text"));
    }
}
