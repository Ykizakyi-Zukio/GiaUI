namespace GiaUI.Data
{
    public class Label
    {
        public string Text { get; set; }
        public Label(string text)
        {
            Text = text;
        }
    }

    public struct ANSI(string name, string color) {
        public string Name { get; set; } = name;
        public string Color { get; set; } = color;
    }

    public struct NS(string name, string text) {
        public string Name { get; set; } = name;
        public string Text { get; set; } = text;

        public static string Find(List<NS> nsList, string name) {
            foreach (var ns in nsList)
                if (ns.Name == name) return ns.Text;
            return "";
        }

        public static void Formatte(List<NS> nsList, string sourceText) {
            foreach (var ns in nsList)
                sourceText = sourceText.Replace($"{{{ns.Name}}}", ns.Text);
        }
    }
}
