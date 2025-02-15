using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

using BasicLibrary;
using LinesLibrary;

namespace ConsoleFormsLibrary {
    public class ColoredLinesArrayPicture : IReadOnlyColoredCharPicture {
        public bool IsTransparent { get; set; }
        public Size Size {
            get {
                int sizeHeight = lines.Length;
                int sizeWidth = lines.Max((line) => line.Line.Length);
                return new Size(sizeHeight, sizeWidth);
            }
        }

        private ColoredLine[] lines;
        public ColoredLine[] Lines {
            get => lines;
            set {
                lines = value ?? throw new ArgumentNullException(nameof(value));
            }
        }



        private ColoredChar EmptyChar = new ColoredChar(' ', Color.White, Color.White);



        public ColoredChar this[int x, int y] {
            get {
                if (x >= Size.Width || y >= Size.Height) {
                    throw new ArgumentOutOfRangeException($"The point ({x}, {y}) was the out of the Size {Size}.");
                }

                ColoredLine coloreLine = lines[y];
                if (lines[y] == null) {
                    return EmptyChar;
                }

                if (x >= coloreLine.Line.Length) {
                    return EmptyChar;
                }

                return coloreLine[x];
            }
        }



        public ColoredLinesArrayPicture() {
            lines = new ColoredLine[0];
        }
        public ColoredLinesArrayPicture(IEnumerable<ColoredLine> lines) {
            Lines = new ColoredLine[lines.Count()];
            Lines.Put(lines);
        }

    }
}
