using GiaUI.Data;

namespace GiaUI.Lib
{
    internal struct Theme(
        Color defCol
    ) {
        public List<Color>? Colors { get; set; }

        public Color DefaultColor = defCol;
        public Color ErrorColor = defCol;
        public Color WarningColor = defCol;
        public Color InfoColor = defCol;

        public List<NS>? NameTexts { get; set; } = null;


    }
}
