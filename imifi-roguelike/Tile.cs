using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace imifi_roguelike
{
    public class Tile
    {
        public string tile_name;
        public int x, y;
        public PictureBox picture;
        ControlCollection Controls;
        public Tile(string tile_name_, int x_, int y_, ControlCollection controls)
        {
            tile_name = tile_name_;
            x = x_;
            y = y_;
            Controls = controls;
            picture = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(16, 16),
                Location = new Point(x, y),
                Image = GameLoop.bitmaps[tile_name],

            };
        }

        public void Draw()
        {
            this.Controls.Add(picture);
        }
    }
}
