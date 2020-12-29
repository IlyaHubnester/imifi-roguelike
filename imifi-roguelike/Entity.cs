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
    public class Entity
    {
        public string sprite;
        public int x;
        public int y;
        public int hp;
        public int maxhp;
        public ControlCollection Controls;
        PictureBox picture;
        public int damage;
        public Weapon weapon;
        public bool visible = false;
        public bool dead = false;
        public bool boss = false;
        public int give_xp;
        public bool drawn;
        public Entity(int start_x, int start_y, ControlCollection controls, string sprite_, Weapon weapon_)
        {
            x = start_x;
            y = start_y;
            drawn = false;
            Controls = controls;
            sprite = sprite_;
            weapon = weapon_;
            damage = weapon.damage;
            picture = new PictureBox
            {
                Name = "pictureBox",
                Size = new Size(16, 16),
                Location = new Point(x, y),
                Image = GameLoop.bitmaps[sprite],
            };        
        }

        public void Update()
        {
            picture.Location = new Point(x, y);
            if (hp <= 0)
            {
                picture.Dispose();
                dead = true;
            }
        }

        public void Draw()
        {
            if (!dead)
            {
                this.Controls.Add(picture);
                this.Controls.SetChildIndex(picture, 0);
            }
        }
    }
}
