using DrawingLibrary;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ConsoleFormsLibrary.Controls.Abstract {
    public abstract class Control : IReadOnlyControl {
        public int Priority { get; set; }
        public virtual Area Area { get; set; }
        public abstract IReadOnlyColoredCharPicture Picture { get; }


        /// <summary>
        /// Defines will <see cref="Control"/> changes its view size <see cref="Size"/>
        /// in area <see cref="Area"/> after rendering <see cref="_Render"/> operation.
        /// </summary>
        public bool AutoSize { get; set; } = true;

        /// <summary>
        /// Defines will <see cref="Control"/> renders itself <see cref="Render"/> operation
        /// after its changes.
        /// </summary>
        public bool AutoRender { get; set; } = true;



        public void Render() {
            _Render();
            if (AutoSize) {
                Area = new Area(Area.Location, Picture.Size);
            }
        }

        protected abstract void _Render();

    }
}
