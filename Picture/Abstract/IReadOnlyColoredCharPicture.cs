using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using LinesLibrary;

namespace ConsoleFormsLibrary {
    public interface IReadOnlyColoredCharPicture {
        bool IsTransparent { get; }
        Size Size { get; }
        ColoredChar this[int x, int y] { get; }

    }
}
