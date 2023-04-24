using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFormsLibrary {
    public interface IColoredCharPictureContainer {
        IReadOnlyColoredCharPicture Picture { get; }
    }
}
