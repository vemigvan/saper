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
        Point coords;
        int minesAround;
        bool isFlagged;

        public Point Coords { get { return coords; } set { coords = value; } }
        public int MinesAround { get { return minesAround; } set { if(value >= 0) minesAround = value; } }
        public bool IsFlagged
        {
            get { return isFlagged; }
            set
            {
                isFlagged = value;
                this.Text = isFlagged ? "P" : "";
            }
        }

        public Cell(int x, int y, int posx, int posy) : base()
        {
            coords = new Point(x, y);

            Location = new Point(posx, posy);
            Size = new Size(40, 40);

        }

        private void Dig()
        {
            Text="wow";
            Enabled = false;
        }

        public delegate void Handler(object sender);
        //public event Handler Click;

    }
}
