using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using ConsoleFormsLibrary.Controls.Abstract;

namespace ConsoleFormsLibrary.Controls {
    public class MulticoloredLabelControl : Control {
        public override IReadOnlyColoredCharPicture Picture => EditablePicture;


        MulticoloredLine text;
        public MulticoloredLine Text {
            get => text;
            set {
                text = value ?? throw new ArgumentNullException(nameof(value));
            }
        }

        public MulticoloredLinesArrayPicture EditablePicture { get; set; }


        public MulticoloredLabelControl() { }
        public MulticoloredLabelControl(MulticoloredLine text) {
            Text = text;
        }



        public override void Render() {
            if (EditablePicture == null) {
                EditablePicture = new MulticoloredLinesArrayPicture(1);
            }

            EditablePicture.MulticoloredLines[0] = Text;
        }

    }
}
