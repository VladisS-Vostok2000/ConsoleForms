using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ConsoleFormsLibrary.Controls.Abstract;
using DrawingLibrary;
using LinesLibrary;

namespace ConsoleFormsLibrary.Controls {
    public sealed class BarControl : Control {
        public override IReadOnlyColoredCharPicture Picture => EditablePicture;

        private int progressPercentage;
        public int ProgressPercentage {
            get => progressPercentage;
            set {
                if (progressPercentage == value) {
                    return;
                }

                progressPercentage = value;
                if (AutoRender) {
                    Render();
                }
            }
        }

        private int barLength;
        /// 
        /// <exception cref="ArgumentException"></exception>
        public int BarLength {
            get => barLength;
            set {
                if (value == barLength) {
                    return;
                }

                if (value < 3) {
                    throw new ArgumentException("Too small width for bar.");
                }

                barLength = value;
                if (AutoRender) {
                    Render();
                }
            }
        }



        private ColoredCharsArrayPicture EditablePicture { get; set; }



        ///
        /// <exception cref="ArgumentException"></exception>
        public BarControl(int barLength) {
            try {
                BarLength = barLength;
            }
            catch (ArgumentException) {
                throw;
            }
        }



        protected override void _Render() {
            Size controlSize = new Size(barLength, 1);
            if (EditablePicture == null || EditablePicture.Size != controlSize) {
                EditablePicture = new ColoredCharsArrayPicture(controlSize);
            }

            int x = 0;
            EditablePicture[x++, 0] = new ColoredChar('[', Color.White, Color.Black);
            int progressCellsCount = GetProgressCellsCount();
            for (int i = 0; i < progressCellsCount; i++) {
                EditablePicture[x++, 0] = new ColoredChar('#', Color.Green, Color.Black);
            }

            for (; x < EditablePicture.Size.Width - 1; x++) {
                EditablePicture[x, 0] = EditablePicture.EmptyChar;
            }

            EditablePicture[barLength - 1, 0] = new ColoredChar(']', Color.White, Color.Black);
        }



        private int GetProgressCellsCount() {
            if (ProgressPercentage <= 0) {
                return 0;
            }
            else
            if (ProgressPercentage >= 100) {
                return barLength - 2;
            }

            return ProgressPercentage * (barLength - 2) / 100;
        }

    }
}
