using BasicLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinesLibrary;

namespace ConsoleFormsLibrary {
    public class MulticoloredLinesArrayPicture : IReadOnlyColoredCharPicture {
        public bool IsTransparent { get; set; }
        public Size Size {
            get {
                int sizeHeight = MulticoloredLines.Length;
                int sizeWidth = MulticoloredLines.Max((line) => line.Length);
                return new Size(sizeWidth, sizeHeight);
            }
        }

        private MulticoloredLine[] multicoloredLines;
        public MulticoloredLine[] MulticoloredLines {
            get => multicoloredLines;
            set {
                multicoloredLines = value ?? throw new ArgumentNullException(nameof(value));
            }
        }


        private ColoredChar EmptyChar = new ColoredChar(' ', Color.White, Color.White);



        public ColoredChar this[int x, int y] {
            get {
                if (x >= Size.Width || y >= Size.Height) {
                    throw new ArgumentOutOfRangeException($"The point ({x}, {y}) was the out of the Size {Size}.");
                }

                MulticoloredLine multicoloreLine = multicoloredLines[y];
                if (multicoloredLines[y] == null) {
                    return EmptyChar;
                }

                if (x >= multicoloreLine.Length) {
                    return EmptyChar;
                }

                return multicoloreLine[x];
            }
        }



        public MulticoloredLinesArrayPicture(int height) {
            multicoloredLines = new MulticoloredLine[height];
        }
        public MulticoloredLinesArrayPicture(IEnumerable<MulticoloredLine> lines) {
            MulticoloredLines = new MulticoloredLine[lines.Count()];
            MulticoloredLines.Put(lines);
        }

    }
}
