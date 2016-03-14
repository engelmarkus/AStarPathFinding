using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pathfinding {
    public class Tile : Button {
        private Content currentState;
        public Content CurrentState {
            get {
                return currentState;
            }

            set {
                currentState = value;
                SetBackground();
            }
        }

        public Tile()
            : base() {
                CurrentState = Content.Empty;
        }

        private void SetBackground() {
            switch (CurrentState) {
                case Content.Empty:
                    this.BackColor = Color.White;
                    break;
                case Content.Start:
                    this.BackColor = Color.Green;
                    break;
                case Content.End:
                    this.BackColor = Color.Red;
                    break;
                case Content.Blocked:
                    this.BackColor = Color.Black;
                    break;
                case Content.Way:
                    this.BackColor = Color.Yellow;
                    break;
            }
        }

        public void IncreaseState() {
            switch (CurrentState) {
                case Content.Empty:
                    CurrentState = Content.Blocked;
                    break;
                case Content.Start:
                    CurrentState = Content.End;
                    break;
                case Content.End:
                    CurrentState = Content.Empty;
                    break;
                case Content.Blocked:
                    CurrentState = Content.Start;
                    break;
                case Content.Way:
                    CurrentState = Content.Empty;
                    break;
            }
        }
    }
}
