using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form
    {
        Field field;
        int difficulty=0;
        Stopwatch sw = new Stopwatch();
        Timer t = new Timer();
        public Form1()
        {
            InitializeComponent();
            this.Load += NewGame;
        }

        public void EndGame(bool isWin)
        {
            sw.Stop();
            Form f = new Form();
            if (isWin)
            {
                f.Text = "Congratulation!";
            }
            else
            {
                f.Text = "Dead :(";
            }
            Button again = new Button();
            again.Text = "Play again!";
            again.Location = new Point(f.Size.Width / 2 - again.Size.Width / 2, f.Size.Height / 2 - again.Size.Height / 2);
            again.Click += NewGame;
            again.Click += Close;

            f.Controls.Add(again);
            f.FormClosed += NewGame;
            f.ShowDialog();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            

            button2.LostFocus += Button2_LostFocus;
            button2.Focus();
            t.Tick += T_Tick;

            easyToolStripMenuItem.Click += NewGame;
            mediumToolStripMenuItem.Click += NewGame;
            hardToolStripMenuItem.Click += NewGame;

            label1.Font = new Font(DefaultFont.FontFamily, 15f, FontStyle.Bold);
            label1.ForeColor = Color.Gray;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            label1.Text = "Time:\n" +sw.Elapsed.ToString("mm\\:ss\\.ff");

            if (field.GameOver)
            {
                t.Stop();
                EndGame(false);
            }
            else if (field.SafeCount == 0)
            {
                t.Stop();
                EndGame(true);
            }
        }

        private void NewGame(object sender, EventArgs e)
        {
            t.Start();
            sw.Restart();

            if (field != null)
            {
                field.Dispose();
            }
            switch(difficulty){
                case 0:
                    field = new Field(8, 8, 25, new Point(5, this.menuStrip1.Size.Height+5));
                    break;
                case 1:
                    field = new Field(10, 10, 35, new Point(5, this.menuStrip1.Size.Height + 5));
                    break;
                case 2:
                    field = new Field(15, 15, 45, new Point(5, this.menuStrip1.Size.Height + 5));
                    break;
            }
            foreach (Cell el in field.Cells)
            {
                this.Controls.Add(el);
            }

            label1.Location = new Point(field.Cells[0, 0].Size.Width * field.W + 15, this.menuStrip1.Size.Height + 5); 
            this.Size = new Size(field.Cells[0, 0].Size.Width * field.W + 137, this.menuStrip1.Size.Height + field.Cells[0, 0].Size.Height * field.H + 50);
        }

        private void Close(object sender, EventArgs e)
        {
            (sender as Button).FindForm().Close();
        }

        private void Button2_LostFocus(object sender, EventArgs e)
        {
            (sender as Button).Focus();
        }

        private void easyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = 0;
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = 1;
        }

        private void hardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            difficulty = 2;
        }
    }
}
