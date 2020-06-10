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
        byte difficulty=0;
        Stopwatch sw = new Stopwatch();
        Timer t = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.LostFocus += Button2_LostFocus;
            button2.Focus();
            t.Start();
            t.Tick += T_Tick;

            easyToolStripMenuItem.Click += NewGame;
            mediumToolStripMenuItem.Click += NewGame;
            hardToolStripMenuItem.Click += NewGame;
        }

        private void T_Tick(object sender, EventArgs e)
        {
            label1.Text = "Time: " +sw.Elapsed.ToString("mm\\:ss\\.ff");
            if(field != null)
            {
                if(field.SafeCount == 0)
                WinGame();
            }
        }

        private void NewGame(object sender, EventArgs e)
        {
            sw.Restart();

            if (field != null)
            {
                field.Dispose();
            }
            switch(difficulty){
                case 0:
                    field = new Field(8, 8, 20, new Point(5, this.menuStrip1.Size.Height+5));
                    break;
                case 1:
                    field = new Field(10, 10, 35, new Point(5, this.menuStrip1.Size.Height + 5));
                    break;
                case 2:
                    field = new Field(15, 15, 45, new Point(5, this.menuStrip1.Size.Height + 5));
                    break;
            }
            this.Size = new Size(400, this.menuStrip1.Size.Height + field.Cells[0, 0].Size.Height * field.H + 50);
            field.Show(this);

            
        }

        public void WinGame()
        {
            sw.Stop();
            Form f = new Form();
            f.ShowDialog();
            MessageBox.Show("You win!", "Congratulations!\n you beat the game in " + label1.Text + " !");
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
