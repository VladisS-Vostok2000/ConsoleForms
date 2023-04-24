using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleFormsLibrary {
    public struct ColoredChar {
        public Color BackgroundColor { get; set; }
        public Color ForegroundColor { get; set; }
        public char Char { get; set; }


        public static readonly ColoredChar Empty = new ColoredChar(' ', Color.White, Color.Black);



        public ColoredChar(char @char, Color foregroundColor, Color backgroundColor) {
            Char = @char;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }



        public override string ToString() => Char.ToString();

    }
}
