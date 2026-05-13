using GiaUI.Data;

namespace GiaUI.Lib
{
    internal struct Theme(
        ANSI defCol
    ) {
        public List<ANSI>? Colors { get; set; }

        public ANSI DefaultColor = defCol;
        public ANSI ErrorColor = defCol;
        public ANSI WarningColor = defCol;
        public ANSI InfoColor = defCol;

        public List<NS>? NameTexts { get; set; } = null;


    }
}
