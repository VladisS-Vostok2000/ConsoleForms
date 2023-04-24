﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;

using BasicLibrary;
using ConsoleFormsLibrary.Controls.Abstract;

namespace ConsoleFormsLibrary.Controls {
    public class RadioControl : Control {
        public override IReadOnlyColoredCharPicture Picture => EditablePicture;

        private IList<MulticoloredLine> options;
        public IList<MulticoloredLine> Options {
            get => options;
            set {
                options = value ?? throw new ArgumentNullException(nameof(value));
            }
        }


        private int selectedOptionIndex;
        public int SelectedOptionIndex {
            get => selectedOptionIndex;
            set {
                selectedOptionIndex = value.ToRange(0, options.Count);
            }
        }


        public MulticoloredLine SelectedOptionLine {
            get {
                if (SelectedOptionIndex >= Options.Count) {
                    return null;
                }

                return Options[SelectedOptionIndex];
            }
        }


        private ColoredCharsArrayPicture EditablePicture { get; set; }


        private readonly ColoredChar unselectedOptionChar = new ColoredChar('*', Color.White, Color.Black);
        private readonly ColoredChar selectedOptionChar = new ColoredChar('*', Color.Red, Color.Black);



        public RadioControl() { }
        public RadioControl(IEnumerable<MulticoloredLine> options) {
            Options = options.ToList();
        }



        public override void Render() {
            Size currentSize = CountSize();
            if (EditablePicture == null || EditablePicture.Size != currentSize) {
                EditablePicture = new ColoredCharsArrayPicture(currentSize);
            }

            int y = 0;
            foreach (var multicoloredLine in Options) {
                int x = 0;
                if (y == SelectedOptionIndex) {
                    EditablePicture[x++, y] = selectedOptionChar;
                }
                else {
                    EditablePicture[x++, y] = unselectedOptionChar;
                }

                EditablePicture[x, y] = ColoredChar.Empty;

                IEnumerator<ColoredChar> enumerator = (multicoloredLine as IEnumerable<ColoredChar>).GetEnumerator();
                while (enumerator.MoveNext()) {
                    EditablePicture[x++, y] = enumerator.Current;
                }

                y++;
            }
        }



        private Size CountSize() {
            return new Size(Options.Max((multicoloredLine) => multicoloredLine.Length) + 2, Options.Count);
        }

    }
}
