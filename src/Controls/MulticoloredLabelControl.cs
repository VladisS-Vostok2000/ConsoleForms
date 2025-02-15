using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

using ConsoleFormsLibrary.Controls.Abstract;
using LinesLibrary;

namespace ConsoleFormsLibrary.Controls {
    public class MulticoloredLabelControl : Control {
        public override IReadOnlyColoredCharPicture Picture => EditablePicture;


        private MulticoloredLine text;
        public MulticoloredLine Text {
            get => text;
            set {
                if (value == null) {
                        throw new ArgumentNullException(nameof(value));
                }

                if (text.Equals(value)) {
                    return;
                }

                text = value;

                if (AutoRender) {
                    Render();
                }
            }
        }

        public MulticoloredLinesArrayPicture EditablePicture { get; set; }


        public MulticoloredLabelControl() {
            text = new ColoredLine(new Line(""), Color.White, Color.Black).AsMulticoloredLine();
        }
        public MulticoloredLabelControl(MulticoloredLine text) {
            if (text == null) {
                throw new ArgumentNullException(nameof(text));
            }

            this.text = text;
        }



        protected override void _Render() {
            if (EditablePicture == null) {
                EditablePicture = new MulticoloredLinesArrayPicture(1);
            }

            EditablePicture.MulticoloredLines[0] = Text;
        }

    }
}
