using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFormsLibrary.Consoles.Abstract {
    public interface IDriver {
        void Print(Point location, ColoredChar coloredChar);

    }
}
