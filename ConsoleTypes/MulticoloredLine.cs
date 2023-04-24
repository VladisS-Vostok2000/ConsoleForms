using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using BasicLibrary;

namespace ConsoleFormsLibrary {
    public class MulticoloredLine : IEnumerable<ColoredLine>, IEnumerable<ColoredChar> {

        public int Length { get; }

        public ColoredLine[] ColoredLines { get; }



        public MulticoloredLine(ColoredLine line) {
            ColoredLines = new ColoredLine[1];
            ColoredLines[0] = line;
            Length = line.Line.Length;
        }
        public MulticoloredLine(IEnumerable<ColoredLine> coloredLines) {
            ColoredLines = new ColoredLine[coloredLines.Count()];
            ColoredLines.Put(coloredLines);
            Length = CountLength(coloredLines);
        }


        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator() => ColoredLines.GetEnumerator();

        #endregion

        #region IEnumerable<ColoredLine>

        public IEnumerator<ColoredLine> GetEnumerator() => ColoredLines.GetEnumerator<ColoredLine>();

        #endregion

        #region IEnumerable<ColoredChar>

        IEnumerator<ColoredChar> IEnumerable<ColoredChar>.GetEnumerator() {
            int currentLineIndex = 0;
            while (true) {
                if (currentLineIndex >= ColoredLines.Length) {
                    yield break;
                }

                ColoredLine currentLine = ColoredLines[currentLineIndex];
                foreach (var coloredChar in currentLine) {
                    yield return coloredChar;
                }

                currentLineIndex++;
            }
        }

        #endregion


        public ColoredChar this[int index] {
            get {
                if (index >= Length) {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                int currentIndex = index;
                for (int i = 0; i < ColoredLines.Length; i++) {
                    if (currentIndex - ColoredLines[i].Line.Length < 0) {
                        return ColoredLines[i][currentIndex];
                    }
                    else {
                        currentIndex -= ColoredLines[i].Line.Length;
                    }
                }

                throw new Exception("Ошибка в коде.");
            }
        }



        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (var coloredLine in ColoredLines) {
                sb.Append(coloredLine.ToString());
            }

            return sb.ToString();
        }



        private int CountLength(IEnumerable<ColoredLine> coloredLines) {
            int length = 0;
            foreach (var coloredLine in coloredLines) {
                length += coloredLine.Line.Length;
            }

            return length;
        }

    }
}
