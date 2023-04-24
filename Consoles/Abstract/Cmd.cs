using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleFormsLibrary.Controls;
using ConsoleFormsLibrary.Controls.Abstract;
using CellsGraphicLibrary;
using static CellsGraphicLibrary.Static;
using DrawingLibrary;
using BasicLibrary;

namespace ConsoleFormsLibrary.Consoles.Abstract {
    public abstract class Cmd {
        public abstract Size Size { get; }

        public ICollection<IReadOnlyControl> Controls { get; } = new HashSet<IReadOnlyControl>();



        public abstract void Visualize();



        protected void Visualize(IDriver driver) {
            List<IReadOnlyControl> controls = Controls.ToList();
            if (controls.Count > 1) {
                controls.Sort((control1, control2) => control1.Priority.CompareTo(control2.Priority));
            }

            foreach (var control in controls) {
                Point consoleTopLeft = new Point(0, 0);
                Area consoleArea = new Area(consoleTopLeft, Size);
                bool inConsole = IsOverlapping(consoleArea, control.Area);
                if (!inConsole) {
                    continue;
                }

                Area controlConsoleArea = GetOverlappingArea(consoleArea, control.Area);
                Visualize(control, consoleTopLeft, new ConsoleArea(controlConsoleArea, driver));
            }
        }



        private void Visualize(IReadOnlyControl control, Point parentLocation, ConsoleArea consoleArea) {
            Area controlConsoleArea = new Area(parentLocation.Sum(control.Area.Location), control.Area.Size);

            bool inConsoleArea = IsOverlapping(consoleArea.Area, controlConsoleArea);
            if (!inConsoleArea) {
                return;
            }

            IReadOnlyColoredCharPicture readOnlyColoredCharPicture = control.Picture;
            if (readOnlyColoredCharPicture != null) {
                VisualizePicture(readOnlyColoredCharPicture, controlConsoleArea, consoleArea);
            }

            if (!(control is IReadOnlyControlContainer)) {
                return;
            }

            IReadOnlyControlContainer controlContainer = (IReadOnlyControlContainer)control;
            var internalControls = controlContainer.GetCurrentControls();
            if (internalControls.Empty()) {
                return;
            }

            var internalControlsArea = controlContainer.InternalArea;
            var consoleInternalControlsArea = new Area(internalControlsArea.Location.Add(controlConsoleArea.Location), internalControlsArea.Size);
            if (!IsOverlapping(consoleArea.Area, consoleInternalControlsArea)) {
                return;
            }

            ConsoleArea consoleAreaInternalControlsArea = consoleArea.SplitOffArea(consoleInternalControlsArea);

            List<IReadOnlyControl> controls = internalControls.ToList();
            if (controls.Count > 1) {
                controls.Sort((control1, control2) => control1.Priority.CompareTo(control2.Priority));
            }

            foreach (var internalControl in controls) {
                Visualize(internalControl, controlConsoleArea.Location, consoleAreaInternalControlsArea);
            }
        }

        private void VisualizePicture(IReadOnlyColoredCharPicture picture, Area consoleControlArea, ConsoleArea consoleArea) {
            // Remember picture may be lower then control size.
            Area consolePictureArea = new Area(consoleControlArea.Location, picture.Size);
            if (!IsOverlapping(consolePictureArea, consoleArea.Area)) {
                return;
            }

            for (int consoleY = consoleControlArea.Location.Y, pictureY = 0; pictureY < picture.Size.Height && pictureY < consoleControlArea.Size.Height && consoleY <= consoleArea.Area.Location.Y.GetBound(consoleArea.Area.Size.Height); pictureY++, consoleY++) {
                for (int consoleX = consoleControlArea.Location.X, pictureX = 0; pictureX < picture.Size.Width && pictureX < consoleControlArea.Size.Width && consoleX <= consoleArea.Area.Location.X.GetBound(consoleArea.Area.Size.Width); pictureX++, consoleX++) {
                    ColoredChar charToPrint = picture[pictureX, pictureY];
                    if (picture.IsTransparent && charToPrint.Char == ' ') {
                        continue;
                    }

                    consoleArea.Print(new Point(consoleX, consoleY), charToPrint);
                }
            }
        }

    }
}
