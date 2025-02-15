using System.Drawing;

using LinesLibrary;

namespace ConsoleFormsLibrary {
    public class ColoredCharsArrayPicture : IReadOnlyColoredCharPicture {
        public bool IsTransparent { get; set; }
        public Size Size { get; }

        private readonly ColoredChar[,] coloredChars;


        public ColoredChar EmptyChar = new ColoredChar(' ', Color.White, Color.Black);



        public ColoredChar this[int x, int y] {
            get => coloredChars[y, x];
            set => coloredChars[y, x] = value;
        }



        public ColoredCharsArrayPicture(Size size) {
            Size = size;
            coloredChars = new ColoredChar[size.Height, size.Width];
            for (int y = 0; y < coloredChars.GetUpperBound(0); y++) {
                for (int x = 0; x < coloredChars.GetUpperBound(1); x++) {
                    coloredChars[y, x] = EmptyChar;
                }
            }
        }

    }
}
