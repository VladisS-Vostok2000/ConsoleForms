using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using ConsoleFormsLibrary.Controls;
using ConsoleFormsLibrary.Controls.Abstract;
using CellsGraphicLibrary;
using static CellsGraphicLibrary.Static;
using DrawingLibrary;
using BasicLibrary;
using LinesLibrary;
using System;

namespace ConsoleFormsLibrary.Consoles.Abstract {
    public abstract class Cmd {
        public abstract Size Size { get; }

        public ICollection<IReadOnlyControl> Controls { get; } = new HashSet<IReadOnlyControl>();



        private IDriver driver;
        protected IDriver Driver {
            get => driver;
            set => driver = value ?? throw new ArgumentNullException(nameof(value));
        }



        protected Cmd(IDriver driver) {
            Driver = driver;
        }



        /// <summary>
        /// Preparing console to visualize controls and drawing they. 
        /// </summary>
        public abstract void Visualize();



        /// <summary>
        /// Drawing existing controls in area.
        /// </summary>
        protected void VisualizeControls() {
            List<IReadOnlyControl> controls = Controls.ToList();

            if (controls.Count > 1) {
                controls.Sort((control1, control2) => control1.Priority.CompareTo(control2.Priority));
            }

            foreach (var control in controls) {
                Point consoleTopLeft = Point.Empty;
                Area consoleArea = new Area(consoleTopLeft, Size);
                bool inConsole = IsOverlapping(consoleArea, control.Area);
                if (!inConsole) {
                    continue;
                }

                Area controlConsoleArea = GetOverlappingArea(consoleArea, control.Area);
                Visualize(control, consoleTopLeft, controlConsoleArea);
            }
        }



        private void Visualize(IReadOnlyControl control, Point parentLocation, Area consoleArea) {
            Area controlAreaLocatedInConsole = new Area(parentLocation.Sum(control.Area.Location), control.Area.Size);

            bool inConsoleArea = IsOverlapping(consoleArea, controlAreaLocatedInConsole);
            if (!inConsoleArea) {
                return;
            }

            IReadOnlyColoredCharPicture readOnlyColoredCharPicture = control.Picture;
            if (readOnlyColoredCharPicture != null) {
                VisualizePicture(readOnlyColoredCharPicture, controlAreaLocatedInConsole, consoleArea);
            }

            if (!(control is IReadOnlyControlContainer)) {
                return;
            }

            IReadOnlyControlContainer controlContainer = control as IReadOnlyControlContainer;
            var internalControls = controlContainer.GetCurrentControls();
            if (internalControls.Empty()) {
                return;
            }

            var internalControlsArea = controlContainer.InternalArea;
            var consoleInternalControlsArea = new Area(internalControlsArea.Location.Add(controlAreaLocatedInConsole.Location), internalControlsArea.Size);
            if (!IsOverlapping(consoleArea, consoleInternalControlsArea)) {
                return;
            }

            Area consoleAreaInternalControlsArea = GetOverlappingArea(consoleArea, consoleInternalControlsArea);

            List<IReadOnlyControl> controls = internalControls.ToList();
            if (controls.Count > 1) {
                controls.Sort((control1, control2) => control1.Priority.CompareTo(control2.Priority));
            }

            foreach (var internalControl in controls) {
                Visualize(internalControl, controlAreaLocatedInConsole.Location, consoleAreaInternalControlsArea);
            }
        }

        private void VisualizePicture(IReadOnlyColoredCharPicture picture, Area consoleControlArea, Area consoleArea) {
            // Remember picture may be lower than control size.
            Area consolePictureArea = new Area(consoleControlArea.Location, picture.Size);
            if (!IsOverlapping(consolePictureArea, consoleArea)) {
                return;
            }

            for (int consoleY = consoleControlArea.Location.Y, pictureY = 0; pictureY < picture.Size.Height && pictureY < consoleControlArea.Size.Height && consoleY <= consoleArea.Location.Y.GetBound(consoleArea.Size.Height); pictureY++, consoleY++) {
                for (int consoleX = consoleControlArea.Location.X, pictureX = 0; pictureX < picture.Size.Width && pictureX < consoleControlArea.Size.Width && consoleX <= consoleArea.Location.X.GetBound(consoleArea.Size.Width); pictureX++, consoleX++) {
                    Point charConsoleLocation = new Point(consoleX, consoleY);
                    if (!IsInRectangle(consoleArea, charConsoleLocation)) {
                        continue;
                    }
                    
                    ColoredChar charToPrint = picture[pictureX, pictureY];
                    if (picture.IsTransparent && charToPrint.Char == ' ') {
                        continue;
                    }

                    Driver.Print(charConsoleLocation, charToPrint);
                }
            }
        }

    }
}
