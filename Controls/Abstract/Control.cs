using DrawingLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleFormsLibrary.Controls.Abstract {
    public abstract class Control : IColoredCharPictureContainer, IReadOnlyControl {
        public int Priority { get; set; }
        public virtual Area Area { get; set; }
        public abstract IReadOnlyColoredCharPicture Picture { get; }





        public abstract void Render();

    }
}
