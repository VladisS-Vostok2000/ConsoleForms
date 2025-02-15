using DrawingLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleFormsLibrary.Controls {
    public interface IReadOnlyControl {
        int Priority { get; }
        Area Area { get; }
        IReadOnlyColoredCharPicture Picture { get; }

    }
}
