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
        bool gameOver;
        Cell[,] cells;
        int h, w, safeCount;
        byte mineFrequency;
        Point location;

        public Cell[,] Cells { get { return cells; } }
        public int H { get { return h; } }
        public int W { get { return w; } }
        public int SafeCount { get { return safeCount; } }
        public int MineFrequency
        {
            set
            {
                if (value > 0 && value <= 255) mineFrequency = (byte)value;
                else mineFrequency = 0;
            }
            get
            {
                return mineFrequency;
            }
        }
        public Point Location { get { return location; } }
        public bool GameOver { get { return gameOver; } }

        public Field(int height, int width, int mineFrequency, Point location)
        {
            gameOver = false;
            this.location = location;
            h = height;
            w = width;
            cells = new Cell[h, w];
            MineFrequency = mineFrequency;
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
                    cells[i, j].MouseDown += Flag;

                    rnd.GetBytes(b);
                    if (b[0] < MineFrequency)
                    {
                        cells[i, j].IsMine = true;
                        cells[i, j].Click += Explode;

                    }
                    else
                    {
                        cells[i, j].EnabledChanged += Cell_EnabledChanged;
                        cells[i, j].Click += Cell_Click1;

                        safeCount++;
                    }
                }
            }

            #region Mines count
            for (int i = 0, index = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++, index++)
                {
                    if (cells[i, j].IsMine)
                    {
                        if (i != 0 && j != w - 1)//not top-right
                        {
                            cells[i - 1, j + 1].MinesAround++;
                        }
                        if (i != 0 && j != 0)//not top-left
                        {
                            cells[i - 1, j - 1].MinesAround++;
                        }
                        if (i != 0)//not top
                        {
                            cells[i - 1, j].MinesAround++;
                        }
                        if (j != 0)//not left
                        {
                            cells[i, j - 1].MinesAround++;
                        }
                        if (i != h - 1 && j != 0)//not bottom-left
                        {
                            cells[i + 1, j - 1].MinesAround++;
                        }
                        if (i != h - 1)//not bottom
                        {
                            cells[i + 1, j].MinesAround++;
                        }
                        if (i != h - 1 && j != w - 1)//not bottom-right
                        {
                            cells[i + 1, j + 1].MinesAround++;
                        }
                        if (j != w - 1)//not right
                        {
                            cells[i, j + 1].MinesAround++;
                        }
                    }
                }
            }
            #endregion
        }
        

        public void Dig(int x, int y)
        {
            if (!cells[x, y].Enabled)
                return;

            cells[x, y].Enabled = false;
            cells[x, y].IsFlagged = false;
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
        private void Flag(object sender, MouseEventArgs e)
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

        private void Cell_EnabledChanged(object sender, EventArgs e)
        {
            Cell cell = (Cell)sender;
            switch (cell.MinesAround)
            {
                case (1):
                    cell.BackColor = Color.FromArgb(233, 255, 181);
                    break;
                case (2):
                    cell.BackColor = Color.FromArgb(168, 255, 181);
                    break;
                case (3):
                    cell.BackColor = Color.FromArgb(151, 242, 239);
                    break;// і так далі
                case (4):
                    cell.BackColor = Color.Red;
                    break;
                case (5):
                    cell.BackColor = Color.Crimson;
                    break;
                case (6):
                    cell.BackColor = Color.Purple;
                    break;
                case (7):
                    cell.BackColor = Color.DarkBlue;
                    break;
                case (8):
                    cell.BackColor = Color.Black;
                    break;
                default:
                    cell.BackColor = Color.FromArgb(204, 204, 204);
                    break;
            }
        }

        private void Explode(object sender, EventArgs e)
        {   if (!(sender as Cell).IsFlagged)
            {
                foreach (Cell el in cells)
                {
                    if (el.IsMine)
                    {
                        el.Text = "*";
                    }
                    gameOver = true;
                }
            }
        }
    }
}

