using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace imifi_roguelike
{

    public partial class Form1 : Form
    {
        GameLoop game_loop;
        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

        }

        private void button1_Click(object sender, EventArgs e)
        {     
            pictureBox1.Dispose();
            button1.Dispose();
            label1.Dispose();
            game_loop = new GameLoop(this.Controls);
        }
    }
}
