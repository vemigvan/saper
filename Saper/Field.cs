using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Drawing;

namespace Saper
{
    class Field : IDisposable
    {
        //fields
        Cell[,] cells;
        int h, w, safeCount;
        byte mineFrequency;
        Point location;

        //prop
        public Cell[,] Cells { get { return cells; } }
        public int H { get; set; }
        public int W { get; set; }
        public int SafeCount { get; }
        public int MineFrequency
        {
            set
            {
                if (value > 0 && value <= 255)
                {
                    mineFrequency = (byte)value;
                }
                else
                {
                    mineFrequency = 0;
                }
            }
            get
            {
                return mineFrequency;
            }
        }
        public Point Location { get; set; }

        //constructor
        public Field(int height, int width, int mineFrequency, Point location)
        {
            Location = location;
            h = height;
            w = width;
            cells = new Cell[h, w];
            MineFrequency = mineFrequency;
            safeCount = h * w;
            Fill();
        }

        //methods
        private void Fill()
        {
            byte[] b = new byte[1];
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            for (int i = 0, index = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++, index++)
                {
                    cells[i, j] = new Cell(i, j);
                    cells[i, j].Location = new Point(this.Location.X + i * 30, this.Location.Y + j * 30);
                    cells[i, j].Size = new Size(30, 30);

                    rnd.GetBytes(b);
                    if (b[0] < MineFrequency)
                    {
                        cells[i, j].IsMine = true;
                        cells[i, j].Click += Explode;
                        safeCount--;
                    }
                    else
                    {
                        
                        cells[i, j].Padding = new Padding(0);
                        cells[i, j].Margin = new Padding(0);
                        cells[i, j].Click += Cell_Click1;
                    }
                    cells[i, j].MouseDown += Field_MouseDown; ;

                }
            }

            #region Mines count
            for (int i = 0, index = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++, index++) { 

                    if (i != 0 && j != w - 1)//not top-right
                    {
                        if (cells[i - 1, j + 1].IsMine) cells[i, j].MinesAround++;
                    }

                if (i != 0 && j != 0)//not top-left
                {
                    if (cells[i - 1, j - 1].IsMine) cells[i, j].MinesAround++;
                }

                if (i != 0)//not top
                {
                    if (cells[i - 1, j].IsMine) cells[i, j].MinesAround++;
                }

                if (j != 0)//not left
                {
                    if (cells[i, j - 1].IsMine) cells[i, j].MinesAround++;
                }

                if (i != h - 1 && j != 0)//not bottom-left
                {
                    if (cells[i + 1, j - 1].IsMine) cells[i, j].MinesAround++;
                }

                if (i != h - 1)//not bottom
                {
                    if (cells[i + 1, j].IsMine) cells[i, j].MinesAround++;
                }

                if (i != h - 1 && j != w - 1)//not bottom-right
                {
                    if (cells[i + 1, j + 1].IsMine) cells[i, j].MinesAround++;
                }

                if (j != w - 1)//not right
                {
                    if (cells[i, j + 1].IsMine) cells[i, j].MinesAround++;
                }
            }
        }
        #endregion
    }

    private void Explode(object sender, EventArgs e)
    {
        MessageBox.Show("heh", "lulzanul");
    }

    public void Show(Form f)
    {
        foreach (Cell e in cells)
        {
            f.Controls.Add(e);
        }
    }

    public void Dig(int x, int y)
    {
        if (!cells[x, y].Enabled)
            return;

        cells[x, y].Enabled = false;

        safeCount--;

        if (cells[x, y].MinesAround != 0)
        {
            cells[x, y].Text = cells[x, y].MinesAround.ToString();
        }
        else
        {
            if (x != 0)
            {
                if (!(cells[x - 1, y].IsMine))
                    Dig(x - 1, y);
            }
            if (y != 0)
            {
                if (!(cells[x, y - 1].IsMine))
                    Dig(x, y - 1);
            }
            if (y != w - 1)
            {
                if (!(cells[x, y + 1].IsMine))
                    Dig(x, y + 1);
            }
            if (x != h - 1)
            {
                if (!(cells[x + 1, y].IsMine))
                    Dig(x + 1, y);
            }

        }



    }

    public void Dispose()
    {
        for (int i = 0; i < h; i++)
        {
            for (int j = 0; j < w; j++)
            {
                cells[i, j].Dispose();
            }
        }
    }
    //events
    private void Field_MouseDown(object sender, MouseEventArgs e)
    {
        if ((e as MouseEventArgs).Button == MouseButtons.Right)
        {
            (sender as Cell).IsFlagged = !(sender as Cell).IsFlagged;
        }
    }

    private void Cell_Click1(object sender, EventArgs e)
    {
        if (!(sender as Cell).IsFlagged)

        {
            Dig((sender as Cell).X, (sender as Cell).Y);
        }
    }





}
    }

