using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using BasicLibrary;
using ConsoleFormsLibrary.Controls.Abstract;
using DrawingLibrary;

using LinesLibrary;

namespace ConsoleFormsLibrary.Controls {
    public class PanelControl : ControlContainer {
        public override IReadOnlyColoredCharPicture Picture => EditablePicture;


        public ColoredChar FrameChar = new ColoredChar('*', Color.White, Color.Black);



        private ColoredCharsArrayPicture EditablePicture { get; set; }



        public PanelControl(Area area) {
            if (area.Size.IsZeroNegativeOrFlat()) {
                throw new ArgumentException();
            }

            if (area.Size.Width < 3 || area.Size.Height < 3) {
                throw new ArgumentException($"Too small size for {nameof(PanelControl)}.");
            }

            Area = area;
            InternalArea = new Area(new Point(1, 1), new Size(Area.Size.Width - 2, Area.Size.Height - 2));
        }



        protected override void _Render() {
            if (EditablePicture == null || EditablePicture.Size != Area.Size) {
                EditablePicture = new ColoredCharsArrayPicture(Area.Size);
            }

            for (int x = 0; x < EditablePicture.Size.Width; x++) {
                EditablePicture[x, 0] = FrameChar;
            }

            for (int y = 1; y < EditablePicture.Size.Height - 1; y++) {
                EditablePicture[0, y] = FrameChar;
                EditablePicture[EditablePicture.Size.Width - 1, y] = FrameChar;
            }

            for (int x = 0; x < EditablePicture.Size.Width; x++) {
                EditablePicture[x, EditablePicture.Size.Height - 1] = FrameChar;
            }
        }

    }
}
