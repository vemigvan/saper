using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    class Mine : Cell
    {
        public Mine(int x, int y, int posx, int posy) : base(x,y,posx,posy)
        {

        }

        //public override void Dig(object sender, EventArgs e)
        //{
        //    (sender as Button).Text = "heh";
        //}
    }
}
