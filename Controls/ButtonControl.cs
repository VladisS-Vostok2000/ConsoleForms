using ConsoleFormsLibrary.Controls.Abstract;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFormsLibrary.Controls {
    public class ButtonControl : Control {
        public override IReadOnlyColoredCharPicture Picture => EditablePicture;
        
        public bool Selected { get; set; }



        private Line Text { get; }
        private ColoredCharsArrayPicture EditablePicture { get; set; }



        public ButtonControl(Line text) {
            Text = text;
        }



        public override void Render() {
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
