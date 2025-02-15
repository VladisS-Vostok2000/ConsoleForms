using ConsoleFormsLibrary.Controls.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LinesLibrary;

namespace ConsoleFormsLibrary.Controls {
    public class ButtonControl : Control {
        public override IReadOnlyColoredCharPicture Picture => EditablePicture;

        private bool selected;
        public bool Selected {
            get => selected;
            set {
                if (selected == value) {
                    return;
                }

                selected = value;
                if (AutoRender) {
                    Render();
                }
            }
        }



        private Line Text { get; }
        private ColoredCharsArrayPicture EditablePicture { get; set; }



        public ButtonControl(Line text) {
            Text = text;
        }



        protected override void _Render() {
            int width = Text.Length + 2;
            if (EditablePicture == null || EditablePicture.Size.Width != width) {
                EditablePicture = new ColoredCharsArrayPicture(new Size(width, 1));
            }

            int x = 0;
            EditablePicture[x++, 0] = new ColoredChar('[', Color.White, Color.Black);
            Color textColor;
            if (Selected) {
                textColor = Color.Green;
            }
            else {
                textColor = Color.White;
            }

            foreach (var @char in Text) {
                EditablePicture[x++, 0] = new ColoredChar(@char, textColor, Color.Black);
            }

            EditablePicture[x, 0] = new ColoredChar(']', Color.White, Color.Black);
        }


    }
}
