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
            minesAround = 0;
            isFlagged = false;
            isMine = false;
        }
    }
}

