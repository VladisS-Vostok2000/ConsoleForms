using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BasicLibrary;

namespace ConsoleFormsLibrary {
    public class Line : IEnumerable<char> {
        public string Text { get; }
        public int Length => Text.Length;



        public char this[int index] {
            get {
                if (index >= Text.Length) {
                    throw new IndexOutOfRangeException($"Index was {index} while line length was {Text.Length}.");
                }

                return Text[index];
            }
        }



        ///
        /// <exception cref="ArgumentException"></exception>
        public Line(string line) {
            if (line.ContainsControlCharacter()) {
                throw new ArgumentException($"{nameof(Line)} does not support control characters.");
            }

            Text = line;
        }



        public override string ToString() {
            return Text;
        }


        #region IEnumerbable<char>

        public IEnumerator<char> GetEnumerator() => Text.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Text.GetEnumerator();

        #endregion

    }
}
