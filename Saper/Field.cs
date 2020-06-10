using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Saper
{
    class Field
    {
        Cell[,] cells;
        int h, w, safeCount = 0;

        public Cell[,] Cells { get; set; }
        public Field(int height, int width)
        {
            h = height;
            w = width;
            cells = new Cell[h, w];
            Fill();
        }
        private void Fill()
        {
            byte[] b = new byte[1];
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            for (int i = 0, index = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++, index++)
                {
                    rnd.GetBytes(b);
                    if (b[0] < 30)
                    {
                        cells[i, j] = new Mine(i, j, i * 40, j * 40);
                        cells[i, j].Text = "";
                    }
                    else
                    {
                        cells[i, j] = new Cell(i, j, i * 40, j * 40);
                        cells[i, j].Click += Cell_Click1;
                        safeCount++;
                    }
                    cells[i, j].MouseDown += Field_MouseDown; ;

                }
            }

            #region Mines count
            for (int i = 0, index = 0; i < h; i++)
            {
                for (int j = 0; j < w; j++, index++)
                {
                    int counter = 0;

                    if (i != 0 && j != w-1)//not top-right
                    {
                        if (cells[i - 1, j + 1] is Mine) counter++;
                    }

                    if (i != 0 && j != 0)//not top-left
                    {
                        if (cells[i - 1, j - 1] is Mine) counter++;
                    }

                    if (i != 0)//not top
                    {
                        if (cells[i - 1, j] is Mine) counter++;
                    }

                    if (j != 0)//not left
                    {
                        if (cells[i, j - 1] is Mine) counter++;
                    }

                    if (i != h-1 && j != 0)//not bottom-left
                    {
                        if (cells[i + 1, j - 1] is Mine) counter++;
                    }

                    if (i != h-1)//not bottom
                    {
                        if (cells[i + 1, j] is Mine) counter++;
                    }

                    if (i != h-1 && j != w-1)//not bottom-right
                    {
                        if (cells[i + 1, j + 1] is Mine) counter++;
                    }

                    if (j != w-1)//not right
                    {
                        if (cells[i, j + 1] is Mine) counter++;
                    }
                    

                    if (counter > 0)
                    {
                        cells[i, j].MinesAround = counter;
                    }
                }
            }
            #endregion
        }

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
                Dig((sender as Cell).Coords.X, (sender as Cell).Coords.Y);
            }
        }


        

        public void Show(Form f)
        {
            foreach (Cell e in cells) {
                f.Controls.Add(e);
            }
        }

        public void Dig(int x, int y)
        {
            if (!cells[x, y].Enabled)
                return;

            cells[x, y].Enabled = false;

            safeCount--;
            if (safeCount == 0)
            {
                MessageBox.Show("You Won!");
                return;
            }

            if (cells[x, y].MinesAround != 0)
            {
                cells[x, y].Text = cells[x, y].MinesAround.ToString();
            }
            else
            {


                if (x != 0)
                {
                    if (!(cells[x - 1, y] is Mine))
                        Dig(x - 1, y);
                }
                if (y != 0)
                {
                    if (!(cells[x, y - 1] is Mine))
                        Dig(x, y - 1);
                }
                if (y != w - 1)
                {
                    if (!(cells[x, y + 1] is Mine))
                        Dig(x, y + 1);
                }
                if (x != h-1)
                {
                    if (!(cells[x + 1, y] is Mine))
                        Dig(x + 1, y);
                }
                
            }

            

        }

    }
}
