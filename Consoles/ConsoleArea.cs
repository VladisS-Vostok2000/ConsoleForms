using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleFormsLibrary.Consoles.Abstract;
using CellsGraphicLibrary;
using static CellsGraphicLibrary.Static;
using DrawingLibrary;

namespace ConsoleFormsLibrary.Consoles {
    public sealed class ConsoleArea {
        public Area Area { get; }



        private IDriver Driver { get; }



        public ConsoleArea(Area area, IDriver driver) {
            Area = area;
            Driver = driver;
        }



        public void Print(Point location, ColoredChar coloredChar) {
            if (!IsInRectangle(Area, location)) {
                return;
            }

            Driver.Print(location, coloredChar);
        }

        ///
        /// <exception cref="ArgumentException"></exception>
        public ConsoleArea SplitOffArea(Area consoleArea) {
            bool overlapping = IsOverlapping(Area, consoleArea);
            if (!overlapping) {
                throw new ArgumentException("Presented area does not overlapping with console area.");
            }

            Area overlappingArea = GetOverlappingArea(consoleArea, Area);
            return new ConsoleArea(overlappingArea, Driver);
        }

    }
}
