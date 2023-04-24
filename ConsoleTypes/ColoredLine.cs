using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleFormsLibrary {
    public class ColoredLine : IEnumerable<ColoredChar> {
        public Line Line { get; }
        public Color ForegroundColor { get; }
        public Color BackgroundColor { get; }



        public ColoredChar this[int index] {
            get {
                if (index >= Line.Length) {
                    throw new ArgumentOutOfRangeException(nameof(index));
                }

                return new ColoredChar(Line[index], ForegroundColor, BackgroundColor);
            }
        }



        public ColoredLine(Line line, Color foregroundColor, Color backgroundColor) {
            Line = line;
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }



        #region IEnumerable

        IEnumerator IEnumerable.GetEnumerator() {
            for (int i = 0; i < Line.Length; i++) {
                yield return new ColoredChar(Line[i], ForegroundColor, BackgroundColor);
            }

            yield break;
        }

        #endregion


        #region IEnumerable<ColoredChar>

        public IEnumerator<ColoredChar> GetEnumerator() {
            for (int i = 0; i < Line.Length; i++) {
                yield return new ColoredChar(Line[i], ForegroundColor, BackgroundColor);
            }

            yield break;
        }

        #endregion



        public override string ToString() => Line.ToString();



        public MulticoloredLine AsMulticoloredLine() {
            return new MulticoloredLine(this);
        }

    }
}
