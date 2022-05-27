
namespace DxLibUI.Views
{
    internal class Text
    {
        public string content;

        public int x;

        public int y;

        public uint color;

        public int fontSize;

        internal Text(string content, int x, int y, uint color, int fontSize = 18)
        {
            this.content = content;
            this.x = x;
            this.y = y;
            this.color = color;
            this.fontSize = fontSize;
        }

    }
}
