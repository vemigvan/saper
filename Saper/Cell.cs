using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    class Cell : Button
    {
        int x,y;
        int minesAround;
        bool isFlagged, isMine;

        public int X { get { return x; } set { x = value > 0 ? value : 0; } }
        public int Y { get { return y; } set { y = value > 0 ? value : 0; } }
        public int MinesAround { get { return minesAround; } set { minesAround = (value >= 0 && value <= 8) ? value : 0; } }
        public bool IsFlagged
        {
            get { return isFlagged; }
            set
            {
                isFlagged = value;
                this.Text = isFlagged ? "P" : "";
            }
        }
        public bool IsMine { get { return isMine; } set { isMine = value; } }

        public Cell(int x, int y) : base()
        {
            X = x;
            Y = y;
            IsFlagged = false;
            MinesAround = 0;

            this.EnabledChanged += Cell_EnabledChanged;

            //this.BackColor = Color.FromArgb(207,240,176);
            this.FlatStyle = FlatStyle.Popup;
            this.Font = new Font(DefaultFont, FontStyle.Bold);
            this.BackColor = Color.FromArgb(205, 214, 186);

        }

        private void Cell_EnabledChanged(object sender, EventArgs e)
        {
            switch (MinesAround)
            {
                case (1):
                    this.BackColor = Color.FromArgb(233,255,181);
                    break;
                case (2):
                    this.BackColor = Color.FromArgb(168, 255, 181);
                    break;
                case (3):
                    this.BackColor = Color.FromArgb(151, 242, 239);
                    break;
                case (4):
                    this.BackColor = Color.Red;
                    break;
                case (5):
                    this.BackColor = Color.Crimson;
                    break;
                case (6):
                    this.BackColor = Color.Purple;
                    break;
                case (7):
                    this.BackColor = Color.DarkBlue;
                    break;
                case (8):
                    this.BackColor = Color.Black;
                    break;
                default:
                    this.BackColor = Color.FromArgb(201,212,231);
                    break;
            }
        }
    }
}
