using DrawingLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFormsLibrary.Controls.Abstract {
    public interface IReadOnlyControlContainer : IReadOnlyControl {
        IReadOnlyCollection<IReadOnlyControl> GetCurrentControls();

        Area InternalArea { get; }

    }
}
