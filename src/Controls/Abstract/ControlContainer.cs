using BasicLibrary;
using DrawingLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleFormsLibrary.Controls.Abstract {
    public abstract class ControlContainer : Control, IReadOnlyControlContainer  {
        private HashSet<Control> controls;
        public HashSet<Control> Controls {
            get => controls;
            set => controls = value ?? throw new ArgumentNullException(nameof(value));
        }


        public Area InternalArea { get; protected set; }



        protected ControlContainer() {
            controls = new HashSet<Control>();
        }
        public ControlContainer(IEnumerable<Control> controls) {
            this.controls = new HashSet<Control>();
            foreach (var control in controls) {
                this.controls.Add(control);
            }
        }



        // TODO: make own readonly collection with covariance.
        public IReadOnlyCollection<IReadOnlyControl> GetCurrentControls() {
            var outCollection = new HashSet<IReadOnlyControl>();
            foreach (var control in controls) {
                outCollection.Add(control);
            }

            return outCollection;
        }

    }
}
